using System.ComponentModel.DataAnnotations;

namespace ThriveKid.API.DTOs.Children
{
    public class UpdateChildDto
    {
        [Required(ErrorMessage = "Child ID is required.")]
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "First name must be 50 characters or fewer.")]
        public string? FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "Last name must be 50 characters or fewer.")]
        public string? LastName { get; set; }

        [RegularExpression("^(Male|Female|Other)$", ErrorMessage = "Gender must be Male, Female, or Other.")]
        public string? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Range(0, 240, ErrorMessage = "Age in months must be between 0 and 240.")]
        public int? AgeInMonths { get; set; }
    }
}
