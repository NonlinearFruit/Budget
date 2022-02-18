using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CategoryClass = Budget.Shared.Category;
using TagClass = Budget.Shared.Tag;

namespace Budget.Shared;

public class Transaction : BaseEntity
{
    [Key]
    public long Id { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public long CategoryId { get; set; }
    public long TagId { get; set; }
    public long AccountId { get; set; }
    public DateTime When { get; set; }

    [ForeignKey(nameof(CategoryId))]
    [InverseProperty(nameof(CategoryClass.Transactions))]
    public virtual Category? Category { get; set; }
    [ForeignKey(nameof(TagId))]
    [InverseProperty(nameof(TagClass.Transactions))]
    public virtual Tag? Tag { get; set; }
    [ForeignKey(nameof(AccountId))]
    [InverseProperty(nameof(BankAccount.Transactions))]
    public virtual BankAccount? Account { get; set; }
}