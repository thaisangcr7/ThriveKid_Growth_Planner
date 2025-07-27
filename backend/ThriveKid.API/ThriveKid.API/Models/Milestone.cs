using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThriveKid.API.Models;

public class Milestone
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public DateTime AchievedDate { get; set; }

    public int ChildId { get; set; }

    public Child? Child { get; set; }
}
