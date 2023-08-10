﻿using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;

namespace BoostProject.ChatsApi.Configuration.HealthChecks;

/// <summary>
/// Health check, which can be used to check the status of a component in the application, such as a backend service, database or some internal
/// </summary>
public class HealthCheck : IHealthCheck
{
    /// <summary>
    /// Checks the BoostProject.Api component on health
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var assembly = Assembly.Load("BoostProject.ChatsApi");
        var versionNumber = assembly.GetName().Version;

        return Task.FromResult(HealthCheckResult.Healthy(description: $"Build {versionNumber}"));
    }
}