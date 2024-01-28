namespace NetworkAPI.Protocol.Util;

public interface IMinecraftReader
{
    
    int ReadVarInt();
    long ReadVarLong();
    string ReadString();
    bool ReadBool();
    int ReadInt();
    short ReadShort();
    ushort ReadUShort();
    double ReadDouble();
    float ReadFloat();
    Guid ReadUuid();
}