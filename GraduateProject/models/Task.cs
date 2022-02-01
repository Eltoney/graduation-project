namespace GraduateProject.models;

public class Task
{
    public int TaskID { set; get; }

    public string ImageName { get; set; }

    public CurrentState CurrentState { get; set; }


    public DateTime AppliedAt { get; set; }

    public int UserID { set; get; }

    public int Result { get; set; }

    public User User { get; set; }
}