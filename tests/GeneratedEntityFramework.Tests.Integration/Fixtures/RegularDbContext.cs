using GeneratedEntityFramework.Tests.Integration.Common;
using Microsoft.EntityFrameworkCore;

namespace GeneratedEntityFramework.Tests.Integration.Fixtures;

public class RegularDbContext(DbContextOptions options) : TestDbContext(options)
{
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Vendor> Vendors { get; set; } = default!;
}
