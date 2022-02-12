using GraduateProject.httpModels;
using GraduateProject.httpModels.response;
using GraduateProject.models;

namespace GraduateProject.services;

public interface ITaskService
{
    /// <summary>
    /// Using this function first to add the task to our database then send the task id to python API and return it to send it to response or return -1 if and only if
    /// the creation of task failed and will update failure message in reference output messgea 
    /// </summary>
    /// <param name="imageLocation">The Actual Image location on disk</param>
    /// <param name="user"> the user who creating that task </param>
    /// <param name="message">the output message</param>
    /// <returns>the task id or -1</returns>
    int CreateTask(string imageLocation, User user, out string message);

    /// <summary>
    /// To Check the current state of a task based on id and to ensure the user is the one who created the task we check the owner of task
    /// NOTE: the task id should be from 1 - INT.MAX_VALUE else will be terminated 
    /// </summary>
    /// <param name="id">the ID of the task</param>
    /// <param name="user">the user who made the request</param>
    /// <param name="message">the output message</param>
    /// <returns>Return Task State object contains all data about task like state and result if found</returns>
    TaskState CheckTask(int id, User user, out string message);
}

class TaskService : ITaskService
{
    public int CreateTask(string imageLocation, User user, out string message)
    {
        throw new NotImplementedException();
    }

    public TaskState CheckTask(int id, User user, out string message)
    {
        throw new NotImplementedException();
    }
}