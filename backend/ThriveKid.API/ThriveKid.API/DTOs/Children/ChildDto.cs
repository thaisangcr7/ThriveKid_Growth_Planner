using System.ComponentModel.DataAnnotations;


namespace ThriveKid.API.DTOs.Children
{
    public class ChildDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName  { get; set; } = string.Empty;
        public string FullName  => $"{FirstName} {LastName}".Trim();

        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = "Unknown";

        // Computed for client
        public int AgeInMonths { get; set; }
    }
}