using GeneratedEntityFramework.Tests.v8.Common;
using Microsoft.EntityFrameworkCore;

namespace GeneratedEntityFramework.Tests.v8.Fixtures;

[GeneratedDbContext<IClassAttributeMappedDbContextCustomers>]
[GeneratedDbContext<IClassAttributeMappedDbContextVendors>]
public partial class ClassAttributeMappedDbContext(DbContextOptions options) : TestDbContext(options);

public interface IClassAttributeMappedDbContextCustomers
{
    public DbSet<Customer> Customers { get; }

    [AsNoTracking]
    public IQueryable<Customer> DbSetCustomersAsNotTracking { get; }
}

public interface IClassAttributeMappedDbContextVendors
{
    public IQueryable<Vendor> Vendors { get; }

    [AsNoTracking]
    public IQueryable<Vendor> QueryableVendorsAsNotTracking { get; }
}
