using GraduateProject.httpModels;
using GraduateProject.httpModels.response;

namespace GraduateProject.services;

public interface ITaskService
{
    bool CreateTask(TaskRequest taskRequest);

    TaskState CheckTask(int id);
}

public class TaskService : ITaskService
{
    public bool CreateTask(TaskRequest taskRequest)
    {
        throw new NotImplementedException();
    }

    public TaskState CheckTask(int id)
    {
        throw new NotImplementedException();
    }
}