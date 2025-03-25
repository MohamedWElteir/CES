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
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DisplayName("Date of Birth")]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [StringLength(14, ErrorMessage = "National ID must be 14 digits")]
    [DisplayName("National ID")]
    public string NationalId { get; init; } = string.Empty;


    [StringLength(11, ErrorMessage = "Phone number must be 11 digits")]
    public string? PhoneNumber { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}