namespace FoodREST.API;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public class Foods
    {
        private const string Base = $"{ApiBase}/foods";

        public const string Create = Base;
        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }
}
