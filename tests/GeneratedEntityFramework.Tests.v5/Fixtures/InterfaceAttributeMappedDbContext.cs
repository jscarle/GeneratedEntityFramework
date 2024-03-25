using GeneratedEntityFramework.Tests.v5.Common;
using Microsoft.EntityFrameworkCore;

namespace GeneratedEntityFramework.Tests.v5.Fixtures;

public partial class InterfaceAttributeMappedDbContext(DbContextOptions options) : TestDbContext(options);

[DbContext(typeof(InterfaceAttributeMappedDbContext))]
public interface IInterfaceAttributeMappedDbContextCustomers
{
    public DbSet<Customer> Customers { get; }

    [AsNoTracking]
    public IQueryable<Customer> DbSetCustomersAsNotTracking { get; }
}

[DbContext(typeof(InterfaceAttributeMappedDbContext))]
public interface IInterfaceAttributeMappedDbContextVendors
{
    public IQueryable<Vendor> Vendors { get; }

    [AsNoTracking]
    public IQueryable<Vendor> QueryableVendorsAsNotTracking { get; }
}
