using System;
using System.Collections.Generic;
using GraduateProject.models;

namespace GraduateProject.contexts
{
    public partial class Token
    {
        public int TokenId { get; set; }
        public int UserId { get; set; }
        public string? Token1 { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public virtual User User { get; set; } = null!;
    }
}