using CES.Models;
using Microsoft.EntityFrameworkCore;

namespace CES.DbContext;

public class MyDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("CES");
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
}