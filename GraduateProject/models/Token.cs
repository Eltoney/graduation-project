namespace GraduateProject.models
{
    public sealed class Token
    {
        public int TokenId { get; set; }
        public int UserId { get; set; }
        public string? Token1 { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public User User { get; set; } = null!;
    }
}