using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SymphonyLimited.Models
{
    public class Auth
    {
        [Key]
        public int AdminID { get; set; }
        public string Name { get; set; }
        public string Username {  get; set; }
        public string Password { get; set; }
        public string CellNo { get; set; }

    }
    public class SymContext: DbContext
    {
        public SymContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Auth>()
                .HasIndex(e => e.Username).IsUnique();
            modelBuilder.Entity<Auth>()
                .HasIndex(e => e.CellNo).IsUnique();
            modelBuilder.Entity<Student>()
                .HasIndex(e => e.StudentID).IsUnique();
            modelBuilder.Entity<Student>()
                .HasIndex(e => e.Email).IsUnique();
            modelBuilder.Entity<Student>()
                .HasIndex(e => e.Phone).IsUnique();
        }
        public DbSet<Auth> Auths { get; set; }
        public DbSet<FAQ> FAQs { get; set; } 
        public DbSet<About> Abouts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}
