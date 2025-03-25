using System.ComponentModel.DataAnnotations;

namespace CES.Models;

public class Enrollment
{

    [Key]
    public Guid EnrollmentId { get; set; }
    public Guid CourseId { get; init; }
    public Guid StudentId { get; init; }

    public Student? Student { get; set; } // navigation property
    public Course? Course { get; set; } // navigation property

}