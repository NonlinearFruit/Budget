using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Budget.Shared;

public class Tag : BaseEntity
{
    [Key]
    public long Id { get; set; }
    public string? Name { get; set; }
    public string Color { get; set; }

    [InverseProperty(nameof(Transaction.Tag))]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}