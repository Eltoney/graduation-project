using System.ComponentModel.DataAnnotations;

namespace GraduateProject.httpModels.api.requests;

public class Request
{
    [Required] public string SessionToken { get; set; }
    
    
}