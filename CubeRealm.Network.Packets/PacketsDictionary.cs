using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using CubeRealm.Network.Packets.Packets.Play.ToClient;
using NetworkAPI.Protocol;
using ConnectionState = Network.ConnectionState;

namespace CubeRealm.Network.Packets;

public class PacketsDictionary
{
    private IList<Type> _handshake = [];
    private IList<Type> _status = [];
    private IList<Type> _login = [];
    private IList<Type> _play = [];

    public IList<Type> GetByConnectionState(ConnectionState state)
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
    
    public IList<Type> Handshake
    {
        get => _handshake;
        init => _handshake = new ReadOnlyCollection<Type>(value);
    }

    public IList<Type> Status
    {
        get => _status;
        init => _status = new ReadOnlyCollection<Type>(value);
    }
    
    public IList<Type> Login
    {
        get => _login;
        init => _login = new ReadOnlyCollection<Type>(value);
    }
    
    public IList<Type> Play
    {
        get => _play;
        init => _play = new ReadOnlyCollection<Type>(value);
    }
}