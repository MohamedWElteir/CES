using System.ComponentModel.DataAnnotations;

namespace CES.Models;

public class Enrollment
{

    [Key]
    public Guid EnrollmentGuid { get; set; }
    public Guid CourseGuid { get; init; }
    public Guid StudentGuid { get; init; }

    public Student? Student { get; set; } // navigation property
    public Course? Course { get; set; } // navigation property

}