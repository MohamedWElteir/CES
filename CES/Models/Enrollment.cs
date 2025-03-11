using System.ComponentModel.DataAnnotations;

namespace CES.Models;

public class Enrollment
{

    [Key]
    public Guid EnrollmentIdGuid { get; set; }
    public Guid CourseIdGuid { get; init; }
    public Guid StudentIdGuid { get; init; }

    public Student? Student { get; set; }
    public Course? Course { get; set; }

}