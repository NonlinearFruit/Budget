using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CategoryClass = Budget.Shared.Category;

namespace Budget.Shared;

public class Forecast : BaseEntity
{
    [Key]
    public long Id { get; set; }
    public decimal Amount { get; set; }
    public string? Notes { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public long CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    [InverseProperty(nameof(CategoryClass.Forecasts))]
    public virtual Category? Category { get; set; }
}