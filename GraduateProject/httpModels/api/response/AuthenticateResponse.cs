using System.Text.Json.Serialization;

namespace GraduateProject.httpModels.api.response;

public class AuthenticateResponse 
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }

    [JsonPropertyName("SessionToken")] public string Token { get; set; }
}