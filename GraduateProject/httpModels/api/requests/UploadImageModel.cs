using System.ComponentModel.DataAnnotations;

namespace GraduateProject.httpModels.api.requests;

//NjM3ODgwMDU3MjI5MzRhbGllbG1vcnN5NzFzRWQ=
public class UploadImageModel
{
    [Required] [DataType(DataType.Upload)] public IFormFile Image { get; set; }

    public byte Gender { get; set; }
    public string SessionToken { get; set; }
}