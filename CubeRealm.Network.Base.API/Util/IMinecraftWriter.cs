namespace NetworkAPI.Protocol.Util;

public interface IMinecraftWriter
{
    
    void Write(byte[] data);
    void WriteByte(byte value);
    int WriteVarInt(int value);
    int WriteVarLong(long value);
    void WriteString(string value);
    void WriteBool(bool value);
    void WriteInt(int value);
    void WriteUShort(ushort value);
    void WriteDouble(double value);
    void WriteFloat(float data);
    void WriteUuid(Guid uuid);
    void WriteByteArray(byte[] values);
    void WriteBuffer(byte[] data);
    void WriteLong(long data);

}