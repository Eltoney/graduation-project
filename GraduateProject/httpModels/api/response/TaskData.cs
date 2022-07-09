using GraduateProject.models;

namespace GraduateProject.httpModels.api.response;

public class TaskData
{
    public int TaskID { get; set; }
    public DateTime TaskDate { get; set; }
    public CurrentState CurrentState { get; set; }
    public double result { get; set; }
    public byte Gender { get; set; }
}