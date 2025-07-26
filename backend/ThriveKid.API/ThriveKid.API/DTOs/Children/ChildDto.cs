namespace ThriveKid.API.DTOs.Children
{
    public class ChildDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int AgeInMonths { get; set; }
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }
}
