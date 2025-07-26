namespace ThriveKid.API.DTOs.Children
{
    public class CreateChildDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public int AgeInMonths { get; set; }
    }
}
