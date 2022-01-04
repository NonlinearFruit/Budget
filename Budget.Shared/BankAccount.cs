using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Budget.Shared;

public class BankAccount
{
    [Key]
    public long Id { get; set; }
    public string? Name { get; set; }
    public string Color { get; set; }
    public decimal LiveTotal { get; set; }
    public DateTime Created { get; set; }

    [InverseProperty(nameof(Transaction.Account))]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}