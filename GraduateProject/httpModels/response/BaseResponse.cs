namespace GraduateProject.httpModels.response;

public class Response
{
    public bool IsSuccess { set; get; }

    public object? Result { set; get; }

    public string Message { set; get; } = "Success";
}