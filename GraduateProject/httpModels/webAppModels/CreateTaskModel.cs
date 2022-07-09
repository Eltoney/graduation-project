using System.ComponentModel.DataAnnotations;

namespace GraduateProject.httpModels.webAppModels;

public class CreateTaskModel
{
    [Required]
    public byte Gender { get; set; }
    public IFormFile File { get; set; }
}