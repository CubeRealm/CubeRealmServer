using System.Collections.ObjectModel;
using CubeRealm.Network.Packets;
using ConnectionState = Network.ConnectionState;

namespace CubeRealm.Network.Base.PacketsBase;

public class PacketsDictionary
{
    private IList<IPacket> _handshake = [];
    private IList<IPacket> _status = [];
    private IList<IPacket> _login = [];
    private IList<IPacket> _play = [];

    public IList<IPacket> GetByConnectionState(ConnectionState state)
    {
        switch (state)
        {
            case ConnectionState.Handshake:
                return Handshake;
            case ConnectionState.Status:
                return Status;
            case ConnectionState.Login:
                return Login;
            case ConnectionState.Play:
                return Play;
        }

        throw new Exception("What?");
    }
    
    public IList<IPacket> Handshake
    {
        get => _handshake;
        init => _handshake = new ReadOnlyCollection<IPacket>(value);
    }

    public IList<IPacket> Status
    {
        get => _status;
        init => _status = new ReadOnlyCollection<IPacket>(value);
    }
    
    public IList<IPacket> Login
    {
        get => _login;
        init => _login = new ReadOnlyCollection<IPacket>(value);
    }
    
    public IList<IPacket> Play
    {
        get => _play;
        init => _play = new ReadOnlyCollection<IPacket>(value);
    }
}