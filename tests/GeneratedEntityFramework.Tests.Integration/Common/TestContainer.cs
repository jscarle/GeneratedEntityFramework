using JetBrains.Annotations;
using Testcontainers.MsSql;

namespace GeneratedEntityFramework.Tests.Integration.Common;

[UsedImplicitly]
public sealed class TestContainer : IAsyncLifetime
{
    private readonly MsSqlContainer _container = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2025-CU7-ubuntu-24.04").Build();

    public string ConnectionString { get; private set; } = default!;

    public async ValueTask InitializeAsync()
    {
        await _container.StartAsync(TestContext.Current.CancellationToken);

        ConnectionString = _container.GetConnectionString();
    }

    public async ValueTask DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}
