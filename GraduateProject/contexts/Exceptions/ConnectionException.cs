using System.Net.Sockets;

namespace GraduateProject.contexts.Exceptions;

public class ConnectionException : Exception
{
    public ConnectionException()
    {
    }

    public ConnectionException(string? message) : base(message)
    {
    }

    public ConnectionException(string message, SocketException socketException) : base(message,socketException)
    {
        
    }
}