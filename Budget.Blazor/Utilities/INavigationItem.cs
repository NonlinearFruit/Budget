namespace Budget.Blazor.Utilities;

public interface INavigationItem
{
    public string Path { get; }
    public string Title { get; }
    public string Icon { get; }
}