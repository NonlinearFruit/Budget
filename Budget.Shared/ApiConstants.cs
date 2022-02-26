namespace Budget.Shared;

public static class ApiConstants
{
    private const string BasePath = "api";
    public static class Forecast
    {
        public const string TestPath = $"{BasePath}/Forecast/Test";

        public static string GetTestsPath(DateTime time)
        {
            return $"{TestPath}?year={time.Year}&month={time.Month}";
        }

        public static string GetClonePath(DateTime from, DateTime to)
        {
            return $"{BasePath}/Forecast/Clone/{from.Year}/{from.Month}/To/{to.Year}/{to.Month}";
        }
    }

    public static class BankAccount
    {
        public const string TestPath = $"{BasePath}/BankAccount/Test";

        public static string GetTestsPath()
        {
            return TestPath;
        }
    }
}