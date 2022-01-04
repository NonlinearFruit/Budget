namespace Budget.Blazor;

public static class DecimalExtensions
{
   private const string CalculationError = "$??";

   public static string ToCurrency(this decimal value)
   {
      if (value*100 - Math.Truncate(value*100) > 0)
         return CalculationError;
      return value < 0
         ? $"(${-value:#,##0.00})"
         : $"${value:#,##0.00}";
   }

   public static string ToColor(this decimal value)
   {
            // new object[] { 0m, "#ffffff" },
            // new object[] { 0.1m, "#d8ffda" },
            // new object[] { 0.01m, "#d8ffda" },
            // new object[] { 0.001m, "#ffffd8" },
            // new object[] { 1000m, "#d8ffda" },
            // new object[] { -1m, "#ffe4e5" },
      if (value == 0)
         return "#ffffff";
      if (value.ToCurrency() == CalculationError)
         return "#ffffd8";
      return value < 0
         ? "#ffe4e5"
         : "#d8ffda";
   }
}