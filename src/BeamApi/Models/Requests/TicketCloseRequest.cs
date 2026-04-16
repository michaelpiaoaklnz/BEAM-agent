using System.ComponentModel.DataAnnotations;

namespace BeamApi.Models.Requests;

public class TicketCloseRequest
{
    [Required]
    public string TicketId { get; set; } = string.Empty;

    [Range(0, int.MaxValue)]
    public int DaysOpen { get; set; }

    public bool IsPriority { get; set; }

    // Perturbed requirement will use this.
    // Original baseline intentionally ignores it.
    public bool ManualClosureRequested { get; set; }
}