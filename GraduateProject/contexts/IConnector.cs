using SimpleConnector.Exceptions;

namespace GraduateProject.contexts;

public interface IConnector : IDisposable
{
    ///  <summary>
    ///  To Start Connection To Simple Connection API on python 
    ///  </summary>
    /// <param name="port">The port used to connect to our API</param>
    ///<throw cref="ConnectionException">if API already connected or failed to connect to API </throw>
   
    void Connect(int port);


    /// <summary>
    ///  To Send Message to API
    /// </summary>
    /// <param name="message">the message to be sent</param>
    /// <throw cref="ConnectionException">If connection Aborted  while sending</throw>
    void SendMessage(string message);

    /// <summary>
    /// Wait for a message from API
    /// </summary>
    /// <returns>the read message</returns>
    /// <throw cref="ConnectionException">If connection Aborted  while Waiting</throw>
    string? WaitForMessage();
}