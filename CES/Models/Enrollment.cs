using System.ComponentModel.DataAnnotations;

namespace CES.Models;

public class Enrollment
{

    [Key]
    public required Guid EnrollmentId { get; set; }
    public required Guid CourseId { get; init; }
    public required Guid StudentId { get; init; }

    public Student? Student { get; init; } // navigation property
    public Course? Course { get; init; } // navigation property

}