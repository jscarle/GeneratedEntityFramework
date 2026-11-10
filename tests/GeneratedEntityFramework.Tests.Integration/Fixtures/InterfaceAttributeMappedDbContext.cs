using GeneratedEntityFramework.Tests.Integration.Common;
using Microsoft.EntityFrameworkCore;

namespace GeneratedEntityFramework.Tests.Integration.Fixtures;

public partial class InterfaceAttributeMappedDbContext(DbContextOptions options) : TestDbContext(options);

[AsNoTracking]
[DbContext<InterfaceAttributeMappedDbContext>]
public interface IInterfaceAttributeMappedDbContextCustomers
{
    public DbSet<Customer> Customers { get; }

    public IQueryable<Customer> DbSetCustomersAsNoTracking { get; }
}

[DbContext<InterfaceAttributeMappedDbContext>]
public interface IInterfaceAttributeMappedDbContextVendors
{
    public IQueryable<Vendor> Vendors { get; }

    [AsNoTracking]
    public IQueryable<Vendor> QueryableVendorsAsNoTracking { get; }
}
