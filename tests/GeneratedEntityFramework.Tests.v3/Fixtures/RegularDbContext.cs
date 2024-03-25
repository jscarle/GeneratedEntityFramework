using GeneratedEntityFramework.Tests.v3.Common;
using Microsoft.EntityFrameworkCore;

namespace GeneratedEntityFramework.Tests.v3.Fixtures;

public class RegularDbContext(DbContextOptions options) : TestDbContext(options)
{
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Vendor> Vendors { get; set; } = default!;
}
