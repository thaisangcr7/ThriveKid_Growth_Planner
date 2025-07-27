using System.ComponentModel.DataAnnotations;

namespace ThriveKid.API.DTOs.Children
{
    public class CreateChildDto
    {
        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50, ErrorMessage = "First name must be 50 characters or fewer.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(50, ErrorMessage = "Last name must be 50 characters or fewer.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [RegularExpression("^(Male|Female|Other)$", ErrorMessage = "Gender must be Male, Female, or Other.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Age in Month is required.")]
        [Range(0, 240, ErrorMessage = "Age in months must be between 0 and 240.")]
        public int AgeInMonths { get; set; }
    }
}
