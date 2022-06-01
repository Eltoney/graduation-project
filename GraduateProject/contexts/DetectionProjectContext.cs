using System;
using System.Collections.Generic;
using GraduateProject.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Task = GraduateProject.models.Task;

namespace GraduateProject.contexts
{
    public partial class DetectionProjectContext : DbContext
    {
        public DetectionProjectContext()
        {
        }

        private DbContextOptions<DetectionProjectContext> _options;

        public DetectionProjectContext(DbContextOptions<DetectionProjectContext> options)
            : base(options)
        {
            this._options = options;
        }

        public virtual DbSet<Token> Tokens { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.userID).HasColumnName("userID");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                        .HasColumnName("emailAddress");

                entity.Property(e => e.Password)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.UserName)
                    .HasMaxLength(32)
                    .HasColumnName("userName")
                    .IsFixedLength();
                entity.Property(e => e.FirstName).HasMaxLength(30).HasColumnName("firstName").IsFixedLength();
                entity.Property(e => e.LastName).HasMaxLength(30).HasColumnName("lastName").IsFixedLength();
            });
            modelBuilder.Entity<Token>(entity =>
            {
                entity.ToTable("tokens");

                entity.Property(e => e.TokenId).HasColumnName("tokenID");

                entity.Property(e => e.Token1)
                    .HasMaxLength(100)
                    .HasColumnName("token")
                    .IsFixedLength();

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(t => t.CreatedDate).HasColumnName("createdDate");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_tokens_Users");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("tasks");

                entity.Property(t => t.TaskID).HasColumnName("taskID");
                entity.Property(t => t.ImageLocation);
                entity.Property(t => t.AppliedAt);
                entity.Property(t => t.UserID);
                entity.Property(t => t.CurrentState);
                entity.Property(t => t.Result);

                entity.HasOne(t => t.User)
                    .WithMany(u => u.Tasks)
                    .HasForeignKey(t => t.UserID)
                    .HasConstraintName("FK_tasks_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}