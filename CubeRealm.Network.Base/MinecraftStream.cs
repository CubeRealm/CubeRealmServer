using System.Net;
using System.Text;
using NetworkAPI.Protocol.Util;
using NetworkAPI.Protocol.Util.Exceptions;

namespace Network;

public class MinecraftStream : Stream, IMinecraftStream
{
    
    public Stream BaseStream { get; private set; }
    
    
    private const int SEGMENT_BITS = 0x7F;
    private const int CONTINUE_BIT = 0x80;

    private CancellationTokenSource CancelationToken => new();

    public MinecraftStream(Stream baseStream)
    {
        BaseStream = baseStream;
    }
    
    public MinecraftStream() : this(new MemoryStream())
    {
    }


    #region OverrideVariables

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

    public override int ReadByte()
    {
        return BaseStream.ReadByte();
    }
    
    public int ReadVarInt(out int bytesRead)
    {
        int numRead = 0;
        int result = 0;
        byte read;
        do
        {
            read = (byte)ReadByte();
            int value = read & 0x7f;
            result |= value << (7 * numRead);
            numRead++;
            if (numRead > 5)
            {
                throw new Exception("VarInt is too big");
            }
        } while ((read & 0x80) != 0);
        bytesRead = numRead;
        return result;
    }

    public int ReadVarInt()
    {
        return ReadVarInt(out _);
    }
    
    public long ReadVarLong()
    {
        int numRead = 0;
        long result = 0;
        byte read;
        do
        {
            read = (byte)ReadByte();
            int value = read & 0x7f;
            result |= (uint) (value << (7 * numRead));
            numRead++;
            if (numRead > 10)
            {
                throw new Exception("VarLong is too big");
            }
        } while ((read & 0x80) != 0);

        return result;
    }

    public string ReadString()
    {
        int length = ReadVarInt();
        byte[] value = Read(length);

        return Encoding.UTF8.GetString(value);
    }

    public bool ReadBool()
    {
        int value = ReadByte();
        return value == 1;
    }

    public int ReadInt()
    {
        byte[] data = Read(4);
        int value = BitConverter.ToInt32(data, 0);

        return IPAddress.NetworkToHostOrder(value);
    }
    
    public short ReadShort()
    {
        byte[] data = Read(2);
        short result = BitConverter.ToInt16(data, 0);
        return IPAddress.NetworkToHostOrder(result);
    }
    public ushort ReadUShort()
    {
        byte[] data = Read(2);
        return NetworkToHostOrder(BitConverter.ToUInt16(data, 0));
    }
    
    public double ReadDouble()
    {
        byte[] value = Read(8);
        return NetworkToHostOrder(value);
    }

    public float ReadFloat()
    {
        byte[] almost = Read(4);
        float data = BitConverter.ToSingle(almost, 0);
        return NetworkToHostOrder(data);
    }
    
    public Guid ReadUuid()
    {
        byte[] long1 = Read(8);
        byte[] long2 = Read(8);
        return new Guid(long1.Concat(long2).ToArray());
    }
    
    public byte[] ReadByteArray()
    {
        int length = ReadVarInt();
        byte[] result = new byte[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = (byte) ReadByte();
        }
        return result;
    }
    
    public byte[] ReadBuffer()
    {
        var length = ReadVarInt(out _);
        var array = new byte[length + 1];
        array[0] = (byte)length;
        _ = Read(array, 1, length);
        return array;
    }
    
    public long ReadLong()
    {
        byte[] l = Read(8);
        return IPAddress.NetworkToHostOrder(BitConverter.ToInt64(l, 0));
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

    public int WriteVarInt(int value)
    {
        var write = 0;
        do
        {
            var temp = (byte)(value & 127);
            value >>= 7;
            if (value != 0)
            {
                temp |= 128;
            }
            WriteByte(temp);
            write++;
        } while (value != 0);
        return write;
    }
    
    public int WriteVarLong(long value)
    {
        int write = 0;
        do
        {
            byte temp = (byte)(value & 127);
            value >>= 7;
            if (value != 0)
            {
                temp |= 128;
            }
            WriteByte(temp);
            write++;
        } while (value != 0);
        return write;
    }

    public void WriteString(string value)
    {
        string text = value ?? string.Empty;
        byte[] data = Encoding.UTF8.GetBytes(text);
        
        WriteVarInt(data.Length);
        Write(data);
    }

    public void WriteBool(bool value)
    {
        Write(BitConverter.GetBytes(value));
    }

    public void WriteInt(int value)
    {
        byte[] buffer = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value));
        Write(buffer);
    }
    
    public void WriteUShort(ushort value)
    {
        byte[] uShortData = BitConverter.GetBytes(value);
        Write(uShortData);
    }
    
    public void WriteDouble(double value)
    {
        Write(HostToNetworkOrder(value));
    }
    
    public void WriteFloat(float data)
    {
        Write(HostToNetworkOrder(data));
    }
    
    public void WriteUuid(Guid uuid)
    {
        byte[] guid = uuid.ToByteArray();
        byte[] long1 = new byte[8];
        byte[] long2 = new byte[8];
        Array.Copy(guid, 0, long1, 0, 8);
        Array.Copy(guid, 8, long2, 0, 8);
        Write(long1);
        Write(long2);
    }
    
    public void WriteByteArray(byte[] values)
    {
        if (values == null)
        {
            WriteVarInt(0);
            return;
        }
        WriteVarInt(values.Length);
        foreach (byte value in values)
            WriteByte(value);
    }
    
    public void WriteBuffer(byte[] data)
    {
        Write(data);
    }
    
    public void WriteLong(long data)
    {
        Write(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(data)));
    }

    
    #endregion


    
    #region Util

    private ushort NetworkToHostOrder(ushort network)
    {
        byte[] net = BitConverter.GetBytes(network);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(net);
        return BitConverter.ToUInt16(net, 0);
    }
    
    private float NetworkToHostOrder(float network)
    {
        byte[] bytes = BitConverter.GetBytes(network);

        if (BitConverter.IsLittleEndian)
            Array.Reverse(bytes);

        return BitConverter.ToSingle(bytes, 0);
    }
    
    private double NetworkToHostOrder(byte[] data)
    {
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(data);
        }
        return BitConverter.ToDouble(data, 0);
    }
    
    private byte[] HostToNetworkOrder(double d)
    {
        byte[] data = BitConverter.GetBytes(d);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(data);
        return data;
    }

    #endregion
    
}