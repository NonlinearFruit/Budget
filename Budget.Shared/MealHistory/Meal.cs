using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Budget.Shared.MealHistory;

[Table(nameof(Meal), Schema="MealHistory")]
public class Meal : BaseEntity
{
    [Key]
    public long Id { get; set; }
    public string Food { get; set; }
    public string MealTime { get; set; }
    public DateTime Date { get; set; }
}