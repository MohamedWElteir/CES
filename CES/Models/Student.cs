using System.ComponentModel.DataAnnotations;

namespace CES.Models;

public class Student
{
    [Key]
    public Guid StudentIdGuid { get; set; }

    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [MaxLength(14)]
    public string NationalId { get; init; } = string.Empty;


    [MaxLength(11)]
    public string PhoneNumber { get; set; } = string.Empty;
}