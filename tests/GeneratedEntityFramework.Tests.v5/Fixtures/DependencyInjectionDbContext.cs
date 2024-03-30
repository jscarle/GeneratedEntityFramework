using GeneratedEntityFramework.Tests.v5.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GeneratedEntityFramework.Tests.v5.Fixtures;

public partial class DependencyInjectionDbContext(DbContextOptions options) : TestDbContext(options);

[AsNoTracking]
[DbContext(typeof(DependencyInjectionDbContext))]
public interface IDependencyInjectionDbContextCustomers : IDependencyInjectionDbContext
{
    public DbSet<Customer> Customers { get; }

    public IQueryable<Customer> DbSetCustomersAsNoTracking { get; }
}

[DbContext(typeof(DependencyInjectionDbContext))]
public interface IDependencyInjectionDbContextVendors : IDependencyInjectionDbContext
{
    public IQueryable<Vendor> Vendors { get; }

    [AsNoTracking]
    public IQueryable<Vendor> QueryableVendorsAsNoTracking { get; }
}

public interface IDependencyInjectionDbContext
{
    public EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        where TEntity : class;

    public EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
        where TEntity : class;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
