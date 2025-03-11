using System.ComponentModel.DataAnnotations;

namespace CES.Models;

public class Course
{

    [Key]
    public Guid CourseIdGuid { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Required]
    public int MaximumCapacity { get; init; }

}