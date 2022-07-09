namespace GraduateProject.models;

public class Task
{
    public Int32 TaskID { set; get; }

    public string ImageLocation { get; set; }

    public CurrentState CurrentState { get; set; } = CurrentState.Idle;


    public DateTime AppliedAt { get; set; } = DateTime.Now;

    public int UserID { set; get; }

    public double Result { get; set; }

    public byte Gender { get; set; }
}