using GeneratedEntityFramework.Tests.v5.Common;
using Microsoft.EntityFrameworkCore;

namespace GeneratedEntityFramework.Tests.v5.Fixtures;

public partial class InterfaceAttributeMappedDbContext(DbContextOptions options) : TestDbContext(options);

[AsNoTracking]
[DbContext(typeof(InterfaceAttributeMappedDbContext))]
public interface IInterfaceAttributeMappedDbContextCustomers
{
    public DbSet<Customer> Customers { get; }

    public IQueryable<Customer> DbSetCustomersAsNoTracking { get; }
}

[DbContext(typeof(InterfaceAttributeMappedDbContext))]
public interface IInterfaceAttributeMappedDbContextVendors
{
    public IQueryable<Vendor> Vendors { get; }

    [AsNoTracking]
    public IQueryable<Vendor> QueryableVendorsAsNoTracking { get; }
}
