using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CES.Models;

public class Student
{
    [Key]
    public Guid StudentIdGuid { get; set; }

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
    [MaxLength(14)]
    [DisplayName("National ID")]
    public string NationalId { get; init; } = string.Empty;


    [MaxLength(11)]
    public string? PhoneNumber { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}