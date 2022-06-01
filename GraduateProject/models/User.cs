using System.Text.Json;
using System.Text.Json.Serialization;
using GraduateProject.contexts;


namespace GraduateProject.models;

public class User
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public int userID { set; get; }

    public string UserName { set; get; }

    public string FirstName { set; get; }

    public string LastName { set; get; }

    public string EmailAddress { set; get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public string? Password { get; set; } = string.Empty;


    public IEnumerable<Token>? Tokens { get; set; }

    public IEnumerable<Task> Tasks { get; set; }
}