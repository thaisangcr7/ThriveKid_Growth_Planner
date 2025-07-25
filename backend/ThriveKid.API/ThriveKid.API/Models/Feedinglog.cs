using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThriveKid.API.Models;

public class FeedingLog
{
    [Key]
    public int Id { get; set; }

    public DateTime FeedingTime { get; set; }

    [Required]
    public string MealType { get; set; }  // e.g., Breastmilk, Formula, Solid

    public string Notes { get; set; }

    // Foreign Key Relationship to Child
    public int ChildId { get; set; }

    [ForeignKey("ChildId")]
    public Child Child { get; set; }
}
