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
    [Range(1, 100)]
    public int MaximumCapacity { get; init; }

}