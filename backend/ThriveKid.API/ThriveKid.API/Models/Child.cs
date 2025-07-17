using System.ComponentModel.DataAnnotations;

namespace ThriveKid.API.Models
{
    public class Child
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public int AgeInMonths { get; set; }
    }
}
