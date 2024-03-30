using JetBrains.Annotations;
using Testcontainers.SqlEdge;

namespace GeneratedEntityFramework.Tests.v6.Common;

[UsedImplicitly]
public sealed class TestContainer : IAsyncLifetime
{
    private readonly SqlEdgeContainer _container = new SqlEdgeBuilder().Build();

    public string ConnectionString { get; private set; } = default!;

    public async Task InitializeAsync()
    {
        await _container.StartAsync();

        ConnectionString = _container.GetConnectionString();
    }

    public async Task DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}
