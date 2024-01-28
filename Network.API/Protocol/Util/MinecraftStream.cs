using System.Text;
using NetworkAPI.Protocol.Util.Exceptions;

namespace NetworkAPI.Protocol.Util;

public class MinecraftStream : Stream
{
    
    public Stream BaseStream { get; private set; }
    
    
    private const int SEGMENT_BITS = 0x7F;
    private const int CONTINUE_BIT = 0x80;

    private CancellationTokenSource CancelationToken => new CancellationTokenSource();

    public MinecraftStream(Stream baseStream)
    {
        BaseStream = baseStream;
    }
    
    public MinecraftStream() : this(new MemoryStream())
    {
    }


    #region OverrideVars

    public override bool CanRead => BaseStream.CanRead;
    public override bool CanSeek => BaseStream.CanSeek;
    public override bool CanWrite => BaseStream.CanWrite;
    public override long Length => BaseStream.Length;
    
    public override long Position
    {
        get => BaseStream.Position;
        set => BaseStream.Position = value;
    }    

    #endregion
    
    

    #region OverrideMethods
    
    public override void Flush()
    {
        BaseStream.Flush();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        return BaseStream.Read(buffer, offset, count);
    }    
    
    public override long Seek(long offset, SeekOrigin origin)
    {
        return BaseStream.Seek(offset, origin);
    }

    public override void SetLength(long value)
    {
        BaseStream.SetLength(value);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        BaseStream.Write(buffer, offset, count);
    }
    
    #endregion
    
    

    #region Reader


    public byte[] Read(int length)
    {
        var s = new SpinWait();
        var read = 0;

        var buffer = new byte[length];
        while (read < buffer.Length && !CancelationToken.IsCancellationRequested &&
               s.Count < 25) 
        {
            var oldRead = read;

            var r = Read(buffer, read, length - read);
            if (r < 0)
            {
                break;
            }

            read += r;

            if (read == oldRead)
            {
                s.SpinOnce();
            }
            if (CancelationToken.IsCancellationRequested) 
                throw new ObjectDisposedException("");
        }

        return buffer;
    }

    public int ReadByte()
    {
        return BaseStream.ReadByte();
    }

    public int ReadVarInt()
    {
        int value = 0;
        int position = 0;
        byte currentByte;

        while (true)
        {
            currentByte = (byte) ReadByte();
            value |= (currentByte & SEGMENT_BITS) << position;

            if ((currentByte & CONTINUE_BIT) == 0) break;
            position += 7;

            if (position >= 32) throw new PacketReadException("VarInt is too big");
        }

        return value;
    }
    
    public long ReadVarLong()
    {
        long value = 0;
        int position = 0;
        byte currentByte;

        while (true)
        {
            currentByte = (byte) ReadByte();
            value |= (long) (currentByte & SEGMENT_BITS) << position;

            if ((currentByte & CONTINUE_BIT) == 0) break;
            position += 7;

            if (position >= 64) throw new PacketReadException("VarInt is too big");
        }

        return value;
    }

    public string ReadString()
    {
        int length = ReadVarInt();
        byte[] value = Read(length);

        return Encoding.UTF8.GetString(value);
    }

    #endregion

    
    
    #region Writer

    public void Write(byte[] data)
    {
        Write(data, 0, data.Length);
    }
    
    public void WriteByte(byte value)
    {
        BaseStream.WriteByte(value);
    }

    public void WriteVarInt(int value)
    {
        while (true)
        {
            if ((value & ~SEGMENT_BITS) == 0)
            {
                WriteByte((byte) value);
                return;
            }
            
            WriteByte((byte) ((value & SEGMENT_BITS) | CONTINUE_BIT));

            value >>>= 7;
        }
    }
    
    public void WriteVarLong(long value)
    {
        while (true)
        {
            if ((value & ~((long) SEGMENT_BITS)) == 0)
            {
                WriteByte((byte) value);
                return;
            }
            
            WriteByte((byte) ((value & SEGMENT_BITS) | CONTINUE_BIT));

            value >>>= 7;
        }
    }

    public void WriteString(string value)
    {
        string text = value ?? string.Empty;
        byte[] data = Encoding.UTF8.GetBytes(text);
        
        WriteVarInt(data.Length);
        Write(data);
    }

    #endregion

    
}