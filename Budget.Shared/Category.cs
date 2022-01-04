using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Budget.Shared;

public class Category
{
    [Key]
    public long Id { get; set; }
    public string? Name { get; set; }
    public string Color { get; set; }
    public DateTime Created { get; set; }

    [InverseProperty(nameof(Forecast.Category))]
    public virtual ICollection<Forecast> Forecasts { get; set; } = new List<Forecast>();

    [InverseProperty(nameof(Transaction.Category))]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}