namespace NetworkAPI.Protocol.Util.Exceptions;

public class PacketReadException : Exception
{

    public PacketReadException()
    {
    }

    public PacketReadException(string message) : base(message)
    {
    }

    public PacketReadException(string message, Exception inner) : base(message, inner)
    {
    }
    
}