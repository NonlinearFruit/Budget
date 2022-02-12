namespace Budget.Shared;

public class ForecastTest
{
    public int Year { get; set; }
    public int Month { get; set; }
    public string CategoryName { get; set; }
    public string CategoryColor { get; set; }
    public string Notes { get; set; }
    public bool SpentLessThanForecasted { get; set; }
    public decimal SpentAmount { get; set; }
    public decimal ForecastedAmount { get; set; }
}