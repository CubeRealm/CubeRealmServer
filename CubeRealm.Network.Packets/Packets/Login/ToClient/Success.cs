using NetworkAPI.Protocol;
using NetworkAPI.Protocol.Util;

namespace NetworkAPI.Protocol.Packets.Login.ToClient;

public class Success : Packet<Success>, IToClient
{
    public byte ClientId => 0x02;

    public Guid Uuid { get; set; }
    public string Username { get; set; }

    public override void Read(IMinecraftStream stream)
    {
        Uuid = stream.ReadUuid();
        Username = stream.ReadString();
    }

    public override void Write(IMinecraftStream stream)
    {
        stream.WriteUuid(Uuid);
        stream.WriteString(Username);
    }
}
