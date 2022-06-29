using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace GraduateProject.httpModels.webAppModels;

[DataContract]
public class SignInModel
{
    [DataMember]
    [DataType(DataType.Text)]
    [StringLength(32)]
    [Required(AllowEmptyStrings = false)]
    public string UserName { get; set; }

    [DataMember]
    [DataType(DataType.Password)]
    [Required(AllowEmptyStrings = false)]
    [StringLength(32)]
    [IgnoreDataMember]
    public string Password { get; set; }
    
}