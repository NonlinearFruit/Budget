namespace Budget.Blazor;

public static class BooleanExtensions
{
    public static string ToIcon(this bool boolean)
    {
        return boolean ? "oi-circle-check" : "oi-circle-x";
    }
}