namespace SwaggerHub.Models
{
    public enum ApiState { Checking, Operational, Slow, Down }

    public record CheckResult (bool Ok, int LatencyMs, DateTime CheckedAt);

    public class ApiStatus
    {
        public const int HistoryLength = 30;
        public const int SlowThresholdMs = 1500;

        public ApiStatus(ApiLink link) => Link = link;

        public ApiLink Link { get; }
        public ApiState State { get; set; } = ApiState.Checking;
        public int? LatencyMs { get; set; }
        public string? Version { get; set; }
        public int? PathCount { get; set; }
        public Dictionary<string, int> Operations { get; set; } = new();
        public List<CheckResult> History { get; } = new();

        public bool IsUp => State is ApiState.Operational or ApiState.Slow;
        public int OperationCount => Operations.Values.Sum();

        public void Record(CheckResult result)
        {
            History.Add(result);
            if (History.Count > HistoryLength)
                History.RemoveAt(0);
        }
    }
}
