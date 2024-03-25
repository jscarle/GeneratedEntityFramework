using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Testcontainers.SqlEdge;

namespace GeneratedEntityFramework.Tests.v7.Common;

[UsedImplicitly]
public sealed class TestContainer<TDbContext> : IAsyncLifetime
    where TDbContext : DbContext
{
    private readonly SqlEdgeContainer _container = new SqlEdgeBuilder().Build();

    public TDbContext DbContext { get; private set; } = default!;

    public async Task InitializeAsync()
    {
        await _container.StartAsync();

        var connectionString = _container.GetConnectionString();

        var builder = new DbContextOptionsBuilder<TDbContext>();
        builder.UseSqlServer(connectionString);
        var dbContext = (TDbContext)(Activator.CreateInstance(typeof(TDbContext), builder.Options) ?? throw new InvalidOperationException());

        await dbContext.Database.EnsureCreatedAsync();

        DbContext = dbContext;
    }

    public async Task DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}
