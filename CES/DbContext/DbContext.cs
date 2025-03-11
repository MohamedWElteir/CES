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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);



        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Student)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.StudentIdGuid)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany()
            .HasForeignKey(e => e.CourseIdGuid);

        var studentIdGuid1 = Guid.NewGuid();
        var studentIdGuid2 = Guid.NewGuid();
        var courseIdGuid1 = Guid.NewGuid();
        var courseIdGuid2 = Guid.NewGuid();

        modelBuilder.Entity<Student>().HasData(
            new Student
            {
                StudentIdGuid = studentIdGuid1,
                FullName = "John Doe",
                Email = "john.doe@example.com",
                DateOfBirth = new DateTime(2000, 1, 1),
                NationalId = "12345678901234",
                PhoneNumber = "12345678901"
            },
            new Student
            {
                StudentIdGuid = studentIdGuid2,
                FullName = "Jane Smith",
                Email = "jane.smith@example.com",
                DateOfBirth = new DateTime(1999, 5, 15),
                NationalId = "23456789012345",

            }
        );


        modelBuilder.Entity<Course>().HasData(
            new Course
            {
                CourseIdGuid = courseIdGuid1,
                Title = "Introduction to Programming",
                Description = "Learn the basics of programming.",
                MaximumCapacity = 30
            },
            new Course
            {
                CourseIdGuid = courseIdGuid2,
                Title = "Advanced Databases",
                Description = "Deep dive into database management systems.",
                MaximumCapacity = 25
            },
            new Course
            {
                CourseIdGuid = Guid.NewGuid(),
                Title = "Advanced Programming",
                MaximumCapacity = 5            }
        );


        modelBuilder.Entity<Enrollment>().HasData(
            new Enrollment
            {
                EnrollmentIdGuid = Guid.NewGuid(),
                StudentIdGuid = Guid.Parse(studentIdGuid1.ToString()),
                CourseIdGuid = Guid.Parse(courseIdGuid1.ToString())
            },
            new Enrollment
            {
                EnrollmentIdGuid = Guid.NewGuid(),
                StudentIdGuid = Guid.Parse(studentIdGuid2.ToString()),
                CourseIdGuid = Guid.Parse(courseIdGuid2.ToString())
            }
        );
    }
}