using System.ComponentModel.DataAnnotations;

namespace Budget.Shared;

public class Check
{
    [Key]
    public long Id { get; set; }
    public int CheckNumber { get; set; }
    public string? Recipient { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public bool Withdrawn { get; set; }
    public DateTime When { get; set; }
    public DateTime Created { get; set; }
}