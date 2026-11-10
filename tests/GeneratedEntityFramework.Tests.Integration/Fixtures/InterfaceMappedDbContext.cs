using GeneratedEntityFramework.Tests.Integration.Common;
using Microsoft.EntityFrameworkCore;

namespace GeneratedEntityFramework.Tests.Integration.Fixtures;

[GeneratedDbContext]
public partial class InterfaceMappedDbContext(DbContextOptions options)
    : TestDbContext(options), IInterfaceMappedDbContextCustomers, IInterfaceMappedDbContextVendors;

[AsNoTracking]
public interface IInterfaceMappedDbContextCustomers
{
    public DbSet<Customer> Customers { get; }

    public IQueryable<Customer> DbSetCustomersAsNoTracking { get; }
}

public interface IInterfaceMappedDbContextVendors
{
    public DbSet<Customer> Customers { get; }

    [AsNoTracking]
    public IQueryable<Customer> DbSetCustomersAsNoTracking { get; }

    public IQueryable<Vendor> Vendors { get; }

    [AsNoTracking]
    public IQueryable<Vendor> QueryableVendorsAsNoTracking { get; }
}
