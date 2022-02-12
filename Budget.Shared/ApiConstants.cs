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
    }
}