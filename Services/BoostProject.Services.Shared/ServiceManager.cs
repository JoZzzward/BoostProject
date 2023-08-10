using BoostProject.Common.Exceptions;
using BoostProject.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BoostProject.Services.Shared;

/// <summary>
/// Service manager helper with common service methods
/// Assistant manager of services with general service methods
/// </summary>
public class ServiceManager
{
    private readonly IDbContextFactory<AppDbContext> _dbContext;

    public ServiceManager(IDbContextFactory<AppDbContext> dbContext)
    {
        _dbContext = dbContext;
    }

    protected async Task IsUsersExists(params Guid?[] userIds)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var usersExists = context
            .Users
            .Any(u => userIds.All(id => id == u.Id));

        ProcessException.ThrowIf(() => usersExists, $"User(s) was not found!");
    }
}