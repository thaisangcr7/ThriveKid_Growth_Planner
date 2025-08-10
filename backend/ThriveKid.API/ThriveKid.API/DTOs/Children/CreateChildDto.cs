using System.ComponentModel.DataAnnotations;

namespace ThriveKid.API.DTOs.Children
{
    public class CreateChildDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName  { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }   // UTC
        public string Gender { get; set; } = "Unknown"; // Unknown | Female | Male | Other
    }
}