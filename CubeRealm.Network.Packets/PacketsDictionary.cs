using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using NetworkAPI.Protocol;
using ConnectionState = Network.ConnectionState;

namespace CubeRealm.Network.Packets;

public class PacketsDictionary
{
    private IDictionary<int, Func<Packet>> _handshake = ReadOnlyDictionary<int, Func<Packet>>.Empty;
    private IDictionary<int, Func<Packet>> _status = ReadOnlyDictionary<int, Func<Packet>>.Empty;
    private IDictionary<int, Func<Packet>> _login = ReadOnlyDictionary<int, Func<Packet>>.Empty;
    private IDictionary<int, Func<Packet>> _play = ReadOnlyDictionary<int, Func<Packet>>.Empty;

    public IDictionary<int, Func<Packet>> GetByConnectionState(ConnectionState state)
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
    
    public IDictionary<int, Func<Packet>> Handshake
    {
        get => _handshake;
        init => _handshake = new ReadOnlyDictionary<int, Func<Packet>>(value);
    }

    public IDictionary<int, Func<Packet>> Status
    {
        get => _status;
        init => _status = new ReadOnlyDictionary<int, Func<Packet>>(value);
    }
    
    public IDictionary<int, Func<Packet>> Login
    {
        get => _login;
        init => _login = new ReadOnlyDictionary<int, Func<Packet>>(value);
    }
    
    public IDictionary<int, Func<Packet>> Play
    {
        get => _play;
        init => _play = new ReadOnlyDictionary<int, Func<Packet>>(value);
    }
}