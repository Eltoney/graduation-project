namespace GraduateProject.services;

public interface ITaskConnectionService
{
    /// <summary>
    /// To Establish Connection With Python API Server 
    /// </summary>
    /// <returns>True if the connection established else false</returns>
    bool Connect();

    bool SendTask();
}

public class TaskConnectionService : ITaskConnectionService
{
    public bool Connect()
    {
        throw new NotImplementedException();
    }

    public bool SendTask()
    {
        throw new NotImplementedException();
    }
}