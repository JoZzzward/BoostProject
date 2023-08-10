using System.Reflection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BoostProject.ResourceServer.Configuration.HealthChecks;

public class SelfHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var assembly = Assembly.Load("BoostProject.ResourceServer");
        var versionNumber = assembly.GetName().Version;

        return Task.FromResult(HealthCheckResult.Healthy(description: $"Build {versionNumber}"));
    }
}