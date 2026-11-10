using Microsoft.EntityFrameworkCore;

namespace GeneratedEntityFramework.Tests.Integration.Common;

public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>();
        modelBuilder.Entity<Vendor>();
    }
}
