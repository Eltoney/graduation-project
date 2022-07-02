using System.Text.Json;
using GraduateProject.contexts;
using GraduateProject.httpModels;
using GraduateProject.httpModels.api.response;
using GraduateProject.models;
using Task = GraduateProject.models.Task;
using SimpleConnector;

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

    /// <summary>
    /// Get List of tasks performed by user
    /// </summary>
    /// <param name="user">Entity contained data about user who performed tasks</param>
    /// <param name="message">A success message</param>
    /// <returns></returns>
    IEnumerable<Task> GetTaskList(User user, out string message);
}

internal class TaskService : ITaskService
{
    private readonly DetectionProjectContext _dbcontext;

    public TaskService(DetectionProjectContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public int CreateTask(string imageLocation, User user, out string message)
    {
        var newTask = new Task
        {
            UserID = user.userID,
            ImageLocation = imageLocation,
            CurrentState = CurrentState.Idle,
            AppliedAt = DateTime.Now,
            Result = -1,
        };


        try
        {
            _dbcontext.Tasks.Add(newTask);
            _dbcontext.SaveChanges();
            var connector = new Connector();
            connector.Connect(41355);
            var json = JsonSerializer.Serialize(newTask);
            connector.SendMessage(json);
            connector.Dispose();

           

            message = "Success";
            return newTask.TaskID;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);            
            message = "Internal Error in AI Detection: -1";
            return -1;
        }
    }

    public TaskState CheckTask(int id, User user, out string message)
    {
        message = "Success";
        var task = _dbcontext.Tasks.SingleOrDefault(t => t.TaskID == id);
        if (task == null)
        {
            message = "No Tasks with such id";
            return new TaskState
            {
                CurrentState = CurrentState.Unknown
            };
        }

        var state = new TaskState
        {
            CurrentState = task.CurrentState
        };
        if (task.CurrentState == CurrentState.Done)
        {
            state.Age = task.Result;
        }

        return state;
    }

    public IEnumerable<Task> GetTaskList(User user, out string message)
    {
        message = "Success";
        var tasks = _dbcontext.Tasks.Where(t => t.UserID == user.userID);

        return tasks;
    }
}