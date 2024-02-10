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

public class MinecraftConnection(ILogger<NetConnection> logger, PacketFactory packetFactory, Socket socket)
    : NetConnection(logger, packetFactory, socket)
{
    private protected override bool CompressionEnabled { get; set; } = false;
    private protected override ConnectionState ConnectionState { get; set; } = ConnectionState.Handshake;
    private protected override int Version { get; set; }

    private protected override void HandlePacket(IPacket packet)
    {
        if (ConnectionState == ConnectionState.Handshake)
        {
            if (packet is Handshake handshake)
            {
                logger.LogTrace("Handshake {} {} {} {}", 
                    handshake.ProtocolVersion,
                    handshake.Address,
                    handshake.Port,
                    handshake.NextState);
                Version = handshake.ProtocolVersion;
                if (handshake.NextState == 1)
                    ConnectionState = ConnectionState.Status;
                if (handshake.NextState == 2)
                    StartLogin();
            }
        }
        else if (ConnectionState == ConnectionState.Status)
        {
            if (packet is Ping ping)
            {
                PacketsQueue.Add(new Ping());
            }
            if (packet is StatusRequest)
            {
                
                PacketsQueue.Add(new StatusResponse
                {
                    JsonString = JsonConvert.SerializeObject(new Motd
                    {
                        Version = new Motd.VersionPart
                        {
                            Name = "1.20.4",
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

    private void StartLogin()
    {
        ConnectionState = ConnectionState.Login;
    }
}