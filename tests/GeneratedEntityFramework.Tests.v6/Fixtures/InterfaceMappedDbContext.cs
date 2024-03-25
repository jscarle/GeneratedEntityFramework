using GeneratedEntityFramework.Tests.v6.Common;
using Microsoft.EntityFrameworkCore;

namespace GeneratedEntityFramework.Tests.v6.Fixtures;

[GeneratedDbContext]
public partial class InterfaceMappedDbContext(DbContextOptions options)
    : TestDbContext(options), IInterfaceMappedDbContextCustomers, IInterfaceMappedDbContextVendors;

public interface IInterfaceMappedDbContextCustomers
{
    public DbSet<Customer> Customers { get; }

    [AsNoTracking]
    public IQueryable<Customer> DbSetCustomersAsNotTracking { get; }
}

public interface IInterfaceMappedDbContextVendors
{
    public DbSet<Customer> Customers { get; }

    [AsNoTracking]
    public IQueryable<Customer> DbSetCustomersAsNotTracking { get; }

    public IQueryable<Vendor> Vendors { get; }

    [AsNoTracking]
    public IQueryable<Vendor> QueryableVendorsAsNotTracking { get; }
}
