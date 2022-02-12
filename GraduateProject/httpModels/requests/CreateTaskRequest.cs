using System.ComponentModel.DataAnnotations;

namespace GraduateProject.httpModels.requests;

public class CreateTaskRequest : Request
{
    [Required]
    public string Image { get; set; }
    
}