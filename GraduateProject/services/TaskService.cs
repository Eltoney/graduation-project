using GraduateProject.httpModels;
using GraduateProject.httpModels.response;
using GraduateProject.models;

namespace GraduateProject.services;

public interface ITaskService
{
    int CreateTask(string imageLocation, User user, out string message);

    TaskState CheckTask(int id);
}

class TaskService : ITaskService
{
    public int CreateTask(string imageLocation, User user, out string message)
    {
        throw new NotImplementedException();
    }

    public TaskState CheckTask(int id)
    {
        throw new NotImplementedException();
    }
}