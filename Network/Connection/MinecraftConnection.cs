using System.Net.Sockets;
using System.Text.Json.Serialization;
using CubeRealm.Network.Packets;
using CubeRealm.Network.Packets.Packets.Handshaking.ToServer;
using CubeRealm.Network.Packets.Packets.Status.ToBoth;
using CubeRealm.Network.Packets.Packets.Status.ToClient;
using CubeRealm.Network.Packets.Packets.Status.ToServer;
using Microsoft.Extensions.Logging;
using NetworkAPI.Protocol;
using Newtonsoft.Json;

namespace Network.Connection;

public class MinecraftConnection : NetConnection
{
    private protected override bool CompressionEnabled { get; set; }
    private protected override ConnectionState ConnectionState { get; set; } = ConnectionState.Handshake;
    private protected override int Version { get; set; }

    public MinecraftConnection(ILogger<NetConnection> logger, PacketFactory packetFactory, Socket socket) :
        base(logger, packetFactory, socket)
    {
        
    }

    private protected override void HandlePacket(Packet packet)
    {
        if (ConnectionState == ConnectionState.Handshake)
        {
            if (packet is Handshake handshake)
            {
                Version = handshake.ProtocolVersion;
                if (handshake.NextState == 2)
                {
                    UnsafeDisconnect();
                }
            }
        }
        else if (ConnectionState == ConnectionState.Status)
        {
            if (packet is Ping ping)
            {
                PacketsQueue.Add(new Ping());
            }
            if (packet is StatusRequest statusRequest)
            {
                PacketsQueue.Add(new StatusResponse
                {
                    JsonString = JsonConvert.SerializeObject(new Motd
                    {
                        Version = new Motd.VersionPart
                        {
                            Name = "1.18.2",
                            Protocol = Version
                        },
                        Players = new Motd.PlayersPart
                        {
                            Max = int.MaxValue,
                            Online = int.MaxValue
                        },
                        Description = new Motd.DescriptionPart
                        {
                            Text = "MDK"
                        },
                        Icon = "",
                        PreviewsChat = false,
                        SecureChat = false
                    })
                });
            }
        }
    }
}