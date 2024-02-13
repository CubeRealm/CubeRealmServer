using System.Collections.ObjectModel;

namespace CubeRealm.Network.Base.API.PacketsBase;

public readonly struct PacketsDictionary
{
    private readonly IList<IPacket> _handshake = [];
    private readonly IList<IPacket> _status = [];
    private readonly IList<IPacket> _login = [];
    private readonly IList<IPacket> _configuration = [];
    private readonly IList<IPacket> _play = [];

    public PacketsDictionary()
    {
    }

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
            case ConnectionState.Configuration:
                return Configuration;
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
    
    public IList<IPacket> Configuration
    {
        get => _configuration;
        init => _configuration = new ReadOnlyCollection<IPacket>(value);
    }
    
    public IList<IPacket> Play
    {
        get => _play;
        init => _play = new ReadOnlyCollection<IPacket>(value);
    }
    
    public static PacketsDictionary operator +(PacketsDictionary a, PacketsDictionary b)
    {
        List<IPacket> handshake = a._handshake.Concat(b._handshake).ToList();
        List<IPacket> status = a._status.Concat(b._status).ToList();
        List<IPacket> login = a._login.Concat(b._login).ToList();
        List<IPacket> configuration = a._configuration.Concat(b._configuration).ToList();
        List<IPacket> play = a._play.Concat(b._play).ToList();
        return new PacketsDictionary
        {
            Handshake = handshake,
            Status = status,
            Login = login,
            Configuration = configuration,
            Play = play
        };
    }
}