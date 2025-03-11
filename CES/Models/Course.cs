using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CES.Models;

public class Course
{

    [Key]
    public Guid CourseIdGuid { get; set; }

    [Required]
    [MaxLength(100)]
    [DisplayName("Course Title")]
    public string Title { get; set; } = string.Empty;


    [MaxLength(int.MaxValue)]
    public string? Description { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    [DisplayName("Maximum Capacity")]
    public int MaximumCapacity { get; init; }

}