namespace SwaggerHub.Models
{
    public static class ApiCatalog
    {
        // SpecUrl must point at the raw OpenAPI JSON; it is fetched from the browser,
        // so it has to be same-origin with the hub (api.mortensens.cc) or send CORS headers.
        public static readonly IReadOnlyList<ApiLink> Apis = new List<ApiLink>
        {
            new("Seinfeld API", "/seinfeld", "https://api.mortensens.cc/seinfeld/swagger", "https://api.mortensens.cc/seinfeld/swagger/v1/swagger.json"),
            new("KnowledgeCenter API", "/kc", "https://api.mortensens.cc/kc/swagger", "https://api.mortensens.cc/kc/swagger/v1/swagger.json"),
            new("OriginPoint API", "/op", "https://api.mortensens.cc/op/swagger/index.html", "https://api.mortensens.cc/op/swagger/v1/swagger.json"),
            new("Brewkeep API", "/brewkeep", "https://api.mortensens.cc/brewkeep/swagger/", "https://api.mortensens.cc/brewkeep/openapi.json"),
            new("Reelkeep API", "/reelkeep", "https://api.mortensens.cc/reelkeep/swagger/index.html", "https://api.mortensens.cc/reelkeep/swagger/v1/swagger.json")
        };
    }
}
