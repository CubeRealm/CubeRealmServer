using System.Net.NetworkInformation;
using System.Net.Sockets;
using CubeRealm.Network.Base.PacketsBase;
using CubeRealm.Network.Packets;
using Microsoft.Extensions.Logging;
using Network;
using Network.Connection;
using Newtonsoft.Json;

namespace CubeRealm.Network.Base.Connection;

public class VersionConnectionBase(ILogger<NetConnection> logger, PacketFactory packetFactory, Socket socket)
    : NetConnection(logger, packetFactory, socket)
{
    private ILogger<NetConnection> Logger { get; } = logger;
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