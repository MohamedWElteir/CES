using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CES.Models;

public sealed class Course
{

    [Key]
    public Guid CourseId { get; set; }

    [Required]
    [MaxLength(100)]
    [DisplayName("Course Title")]
    public required string Title { get; init; }


    [MaxLength(int.MaxValue)]
    public string? Description { get; init; }

    [Required]
    [Range(1, int.MaxValue)]
    [DisplayName("Maximum Capacity")]
    public required int MaximumCapacity { get; init; }

}