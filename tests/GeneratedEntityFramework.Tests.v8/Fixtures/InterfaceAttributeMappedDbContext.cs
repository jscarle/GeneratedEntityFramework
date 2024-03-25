using GeneratedEntityFramework.Tests.v8.Common;
using Microsoft.EntityFrameworkCore;

namespace GeneratedEntityFramework.Tests.v8.Fixtures;

public partial class InterfaceAttributeMappedDbContext(DbContextOptions options) : TestDbContext(options);

[DbContext<InterfaceAttributeMappedDbContext>]
public interface IInterfaceAttributeMappedDbContextCustomers
{
    public DbSet<Customer> Customers { get; }

    [AsNoTracking]
    public IQueryable<Customer> DbSetCustomersAsNotTracking { get; }
}

[DbContext<InterfaceAttributeMappedDbContext>]
public interface IInterfaceAttributeMappedDbContextVendors
{
    public IQueryable<Vendor> Vendors { get; }

    [AsNoTracking]
    public IQueryable<Vendor> QueryableVendorsAsNotTracking { get; }
}
