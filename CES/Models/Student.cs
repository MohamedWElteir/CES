using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CES.Models;

public sealed class Student
{
    [Key]
    public Guid StudentId { get; set; }

    [Required]
    [MaxLength(100)]
    [DisplayName("Full Name")]
    public required string FullName { get; init; }

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public required string Email { get; init; }

    [Required]
    [DisplayName("Date of Birth")]
    public required DateTime DateOfBirth { get; init; }

    [Required]
    [StringLength(14, ErrorMessage = "National ID must be 14 digits")]
    [DisplayName("National ID")]
    public string NationalId { get; init; } = string.Empty;


    [StringLength(11, ErrorMessage = "Phone number must be 11 digits")]
    public string? PhoneNumber { get; init; }

    public ICollection<Enrollment> Enrollments { get; init; } = new List<Enrollment>();
}