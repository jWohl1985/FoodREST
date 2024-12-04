namespace FoodREST.API;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public class Foods
    {
        private const string Base = $"{ApiBase}/foods";

        public const string Create = Base;
        public const string Get = $"{ApiBase}/foods/{{id:guid}}";
        public const string GetAll = $"{ApiBase}/foods";
    }
}
