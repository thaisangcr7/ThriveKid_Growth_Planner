using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThriveKid.API.Models;

public class Milestone
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Description { get; set; }

    public DateTime AchievedDate { get; set; }

    // Foreign Key Relationship to Child
    public int ChildId { get; set; }

    [ForeignKey("ChildId")]
    public Child Child { get; set; }
}
