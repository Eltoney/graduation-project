using System.ComponentModel.DataAnnotations;

namespace GraduateProject.httpModels;

public class Request
{
    [Required] public string SessionToken { get; set; }

    public object Data { get; set; }
}