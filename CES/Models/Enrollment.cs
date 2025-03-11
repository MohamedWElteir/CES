using System.ComponentModel.DataAnnotations;

namespace CES.Models;

public class Enrollment
{

    [Key]
    public Guid EnrollmentIdGuid { get; set; }
    public Guid CourseIdGuid { get; set; }
    public Guid StudentIdGuid { get; set; }

}