namespace FoodREST.API;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public class Foods
    {
        private const string Base = $"{ApiBase}/foods";

        public const string Create = Base;
    }
}
