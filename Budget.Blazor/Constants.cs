namespace Budget.Blazor;

public static class Constants
{
    public const string HomePath = "/";

    public static class BankAccount
    {
        public const string CataloguePath = "/Accounts";
        public const string CreatorPath = "/CreateAccount";
        public const string EditorPath = "/EditAccount/{Id:long}";
        public const string ViewerPath = "/Account/{Id:long}";
        public static string GetEditorPath(long id) => $"/EditAccount/{id}";
        public static string GetViewerPath(long id) => $"/Account/{id}";
    }

    public static class Tag
    {
        public const string CataloguePath = "/Tags";
        public const string CreatorPath = "/CreateTag";
        public const string EditorPath = "/EditTag/{Id:long}";
        public const string ViewerPath = "/Tag/{Id:long}";
        public static string GetEditorPath(long id) => $"/EditTag/{id}";
        public static string GetViewerPath(long id) => $"/Tag/{id}";
    }

    public static class Forecast
    {
        public const string CataloguePath = "/Forecasts";
        public const string ClonePath = "/Forecasts/Clone";
        public const string UpsertPath = "/UpsertForecast/{Id:long?}";
        public const string ViewerPath = "/Forecast/{Id:long}";
        public static string GetUpsertPath(long? id = null) => $"/UpsertForecast/{id?.ToString() ?? ""}";
        public static string GetViewerPath(long id) => $"/Forecast/{id}";
    }

    public static class Transaction
    {
        public const string CataloguePath = "/Transactions";
        public const string UpsertPath = "/UpsertTransaction/{Id:long?}";
        public const string ViewerPath = "/Transaction/{Id:long}";
        public static string GetUpsertPath(long? id = null) => $"/UpsertTransaction/{id?.ToString() ?? ""}";
        public static string GetViewerPath(long id) => $"/Transaction/{id}";
    }

    public static class Category
    {
        public const string CataloguePath = "/Categories";
        public const string CreatorPath = "/CreateCategory";
        public const string EditorPath = "/EditCategory/{Id:long}";
        public const string ViewerPath = "/Category/{Id:long}";
        public static string GetEditorPath(long id) => $"/EditCategory/{id}";
        public static string GetViewerPath(long id) => $"/Category/{id}";
    }

    public static class Check
    {
        public const string CataloguePath = "/Checks";
        public const string UpsertorPath = "/UpsertCheck/{Id:long?}";
        public const string ViewerPath = "/Check/{Id:long}";
        public static string GetUpsertPath(long? id = null) => $"/UpsertCheck/{id?.ToString() ?? ""}";
        public static string GetViewerPath(long id) => $"/Check/{id}";
    }

    public static class Meal
    {
        public const string CataloguePath = "/Meals";
        public const string UpsertorPath = "/UpsertMeal/{Id:long?}";
        public const string ViewerPath = "/Meal/{Id:long}";
        public static string GetUpsertPath(long? id = null) => $"/UpsertMeal/{id?.ToString() ?? ""}";
        public static string GetViewerPath(long id) => $"/Meal/{id}";
    }

    public static class Api
    {
        public static string GetTransactions()
        {
            return "Api/Transaction";
        }

        public static string GetTransactions(Budget.Shared.Tag tag)
        {
            return $"{GetTransactions()}?tag={tag.Id}";
        }

        public static string GetTransactions(Budget.Shared.Category category)
        {
            return $"{GetTransactions()}?category={category.Id}";
        }

        public static string GetTransactions(Budget.Shared.Forecast forecast)
        {
            return $"{GetTransactions()}?year={forecast.Year}&month={forecast.Month}&category={forecast.CategoryId}";
        }

        public static string GetTransactions(Budget.Shared.BankAccount account)
        {
            return $"{GetTransactions()}?account={account.Id}";
        }
    }
}