namespace SwaggerHub.Models
{
    public class ApiGroup
    {
        public string? GroupName { get; set; }
        public List<ApiLink>? Links { get; set; } = new();
    }
}
