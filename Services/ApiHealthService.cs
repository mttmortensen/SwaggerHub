using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using SwaggerHub.Models;

namespace SwaggerHub.Services
{
    // Checks an API by fetching its OpenAPI spec: the fetch time is the response time,
    // and the spec itself gives the version and endpoint/operation counts.
    public class ApiHealthService(HttpClient http)
    {
        public static readonly string[] Methods = { "get", "post", "put", "patch", "delete" };

        public async Task CheckAsync(ApiStatus status)
        {
            try
            {
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                using var request = new HttpRequestMessage(HttpMethod.Get, status.Link.SpecUrl);
                request.SetBrowserRequestCache(BrowserRequestCache.NoStore);

                var stopwatch = Stopwatch.StartNew();
                using var response = await http.SendAsync(request, cts.Token);
                var latency = (int)stopwatch.ElapsedMilliseconds;
                response.EnsureSuccessStatusCode();

                await using var stream = await response.Content.ReadAsStreamAsync(cts.Token);
                using var doc = await JsonDocument.ParseAsync(stream, cancellationToken: cts.Token);
                ReadSpec(status, doc.RootElement);

                status.LatencyMs = latency;
                status.State = latency > ApiStatus.SlowThresholdMs ? ApiState.Slow : ApiState.Operational;
                status.Record(new CheckResult(true, latency, DateTime.Now));
            }
            catch (Exception) // network/CORS failure, timeout, non-2xx, or a body that isn't JSON
            {
                status.LatencyMs = null;
                status.State = ApiState.Down;
                status.Record(new CheckResult(false, 0, DateTime.Now));
            }
        }

        private static void ReadSpec(ApiStatus status, JsonElement root)
        {
            if (root.TryGetProperty("info", out var info) && info.TryGetProperty("version", out var version))
                status.Version = version.GetString();

            var operations = Methods.ToDictionary(m => m, _ => 0);
            var pathCount = 0;
            if (root.TryGetProperty("paths", out var paths) && paths.ValueKind == JsonValueKind.Object)
            {
                foreach (var path in paths.EnumerateObject())
                {
                    pathCount++;
                    if (path.Value.ValueKind != JsonValueKind.Object) continue;
                    foreach (var op in path.Value.EnumerateObject())
                        if (operations.ContainsKey(op.Name))
                            operations[op.Name]++;
                }
            }

            status.PathCount = pathCount;
            status.Operations = operations;
        }
    }
}
