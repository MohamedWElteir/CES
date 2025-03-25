﻿using CES.Models;
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
            .HasForeignKey(e => e.StudentGuid)
            .OnDelete(DeleteBehavior.ClientNoAction);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany()
            .HasForeignKey(e => e.CourseGuid)
            .OnDelete(DeleteBehavior.ClientNoAction);

        var studentIdGuid1 = Guid.NewGuid();
        var studentIdGuid2 = Guid.NewGuid();
        var courseIdGuid1 = Guid.NewGuid();
        var courseIdGuid2 = Guid.NewGuid();

        modelBuilder.Entity<Student>().HasData(
            new Student
            {
                StudentGuid = studentIdGuid1,
                FullName = "John Doe",
                Email = "john.doe@example.com",
                DateOfBirth = new DateTime(2000, 1, 1),
                NationalId = "12345678901234",
                PhoneNumber = "12345678901"
            },
            new Student
            {
                StudentGuid = studentIdGuid2,
                FullName = "Jane Smith",
                Email = "jane.smith@example.com",
                DateOfBirth = new DateTime(1999, 5, 15),
                NationalId = "23456789012345",

            }
        );


        modelBuilder.Entity<Course>().HasData(
            new Course
            {
                CourseGuid = courseIdGuid1,
                Title = "Introduction to Programming",
                Description = "Learn the basics of programming.",
                MaximumCapacity = 30
            },
            new Course
            {
                CourseGuid = courseIdGuid2,
                Title = "Advanced Databases",
                Description = "Deep dive into database management systems.",
                MaximumCapacity = 25
            },
            new Course
            {
                CourseGuid = Guid.NewGuid(),
                Title = "Advanced Programming",
                MaximumCapacity = 5            }
        );


        modelBuilder.Entity<Enrollment>().HasData(
            new Enrollment
            {
                EnrollmentGuid = Guid.NewGuid(),
                StudentGuid = Guid.Parse(studentIdGuid1.ToString()),
                CourseGuid = Guid.Parse(courseIdGuid1.ToString())
            },
            new Enrollment
            {
                EnrollmentGuid = Guid.NewGuid(),
                StudentGuid = Guid.Parse(studentIdGuid2.ToString()),
                CourseGuid = Guid.Parse(courseIdGuid2.ToString())
            }
        );
    }
}