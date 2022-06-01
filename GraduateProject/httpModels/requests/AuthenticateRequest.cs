using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GraduateProject.httpModels;

public class AuthenticateRequest
{
    [Required] public string Username { get; set; }

    [Required] public string? Password { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string FirstName { get; set; } = String.Empty;

    public string LastName { get; set; } = String.Empty;

    public string EmailAddress { get; set; } = String.Empty;

    public string ToString()
    {
        return $"FirstName: {FirstName}, LastEnd: {LastName}, UserName: {Username}, EmailAddress: {EmailAddress}";
    }
}