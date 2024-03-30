using GeneratedEntityFramework.Tests.v8.Common;
using Microsoft.EntityFrameworkCore;

namespace GeneratedEntityFramework.Tests.v8.Fixtures;

[GeneratedDbContext<IClassAttributeMappedDbContextCustomers>]
[GeneratedDbContext<IClassAttributeMappedDbContextVendors>]
public partial class ClassAttributeMappedDbContext(DbContextOptions options) : TestDbContext(options);

[AsNoTracking]
public interface IClassAttributeMappedDbContextCustomers
{
    public DbSet<Customer> Customers { get; }

    public IQueryable<Customer> DbSetCustomersAsNoTracking { get; }
}

public interface IClassAttributeMappedDbContextVendors
{
    public IQueryable<Vendor> Vendors { get; }

    [AsNoTracking]
    public IQueryable<Vendor> QueryableVendorsAsNoTracking { get; }
}
