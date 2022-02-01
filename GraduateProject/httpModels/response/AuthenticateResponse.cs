using System.Text.Json.Serialization;

namespace GraduateProject.httpModels.response;

public class AuthenticateResponse : IResult
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }

    [JsonPropertyName("Session_Token")] public string Token { get; set; }
}