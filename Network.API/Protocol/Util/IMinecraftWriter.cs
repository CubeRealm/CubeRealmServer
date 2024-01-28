namespace NetworkAPI.Protocol.Util;

public interface IMinecraftWriter
{

    void WriteByte(byte value);
    void WriteVarInt(int value);
    void WriteVarLong(long value);
    void WriteString(string value);
    void WriteBool(bool value);
    void WriteInt(int value);
    void WriteUShort(ushort value);
    void WriteDouble(double value);
    void WriteFloat(float data);
    void WriteUuid(Guid uuid);
    void WriteByteArray(byte[] values);

}