using FluentAssertions;
using GeneratedEntityFramework.Tests.v7.Common;
using GeneratedEntityFramework.Tests.v7.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace GeneratedEntityFramework.Tests.v7;

public sealed class InterfaceAttributeMappedTests(TestContainer container) : TestBase, IAsyncLifetime
{
    private static readonly AsyncLock Mutex = new();
    private static bool _seeded;
    private static InterfaceAttributeMappedDbContext _dbContext = default!;

    public async Task InitializeAsync()
    {
        using (await Mutex.LockAsync())
        {
            if (_seeded)
                return;

            var builder = new DbContextOptionsBuilder<InterfaceAttributeMappedDbContext>();
            builder.UseSqlServer(container.ConnectionString);
            _dbContext = new InterfaceAttributeMappedDbContext(builder.Options);

            await _dbContext.Database.EnsureCreatedAsync();

            _dbContext.AddRange(GetCustomersSeedData());
            _dbContext.AddRange(GetVendorsSeedData());
            await _dbContext.SaveChangesAsync();

            _seeded = true;
        }
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public async Task ShouldSeedCustomer()
    {
        var seededCustomers = await _dbContext.Customers.Where(x => x.Id >= 1 && x.Id <= 5).ToListAsync();

        seededCustomers.Count.Should().Be(5);
    }

    [Fact]
    public async Task ShouldSaveCustomer()
    {
        _dbContext.Add(new Customer { Id = 42, Name = "Douglas Adams" });
        await _dbContext.SaveChangesAsync();
        var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == 42);

        customer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 42, Name = "Douglas Adams" });
    }

    [Fact]
    public async Task ShouldUpdateCustomer()
    {
        var existingCustomer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == 6);

        existingCustomer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 6, Name = "Isabella Khan" });

        existingCustomer!.Name = "Jennifer Moris";
        await _dbContext.SaveChangesAsync();
        var updatedCustomer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == 6);

        updatedCustomer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 6, Name = "Jennifer Moris" });
    }

    [Fact]
    public async Task ShouldDeleteCustomer()
    {
        var existingCustomer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == 7);

        existingCustomer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 7, Name = "Lucas Singh" });

        _dbContext.Remove(existingCustomer!);
        await _dbContext.SaveChangesAsync();
        var deletedCustomer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == 7);

        deletedCustomer.Should().BeNull();
    }

    [Fact]
    public async Task ShouldNotTrackCustomer()
    {
        var existingCustomer = await _dbContext.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 8);

        existingCustomer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Sophia Wang" });

        existingCustomer!.Name = "Jennifer Moris";
        await _dbContext.SaveChangesAsync();
        var updatedCustomer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == 8);

        updatedCustomer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Sophia Wang" });
    }

    [Fact]
    public async Task ShouldNotTrackCustomerWithAsNoTrackAttribute()
    {
        var existingCustomer = await _dbContext.DbSetCustomersAsNoTracking.FirstOrDefaultAsync(x => x.Id == 8);

        existingCustomer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Sophia Wang" });

        existingCustomer!.Name = "Jennifer Moris";
        await _dbContext.SaveChangesAsync();
        var updatedCustomer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == 8);

        updatedCustomer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Sophia Wang" });
    }

    [Fact]
    public async Task ShouldSeedVendor()
    {
        var seededVendors = await _dbContext.Vendors.Where(x => x.Id >= 1 && x.Id <= 5).ToListAsync();

        seededVendors.Count.Should().Be(5);
    }

    [Fact]
    public async Task ShouldSaveVendor()
    {
        _dbContext.Add(new Vendor { Id = 42, Name = "Galaxy Dreams" });
        await _dbContext.SaveChangesAsync();
        var customer = await _dbContext.Vendors.FirstOrDefaultAsync(x => x.Id == 42);

        customer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 42, Name = "Galaxy Dreams" });
    }

    [Fact]
    public async Task ShouldUpdateVendor()
    {
        var existingVendor = await _dbContext.Vendors.FirstOrDefaultAsync(x => x.Id == 6);

        existingVendor.Should().NotBeNull().And.BeEquivalentTo(new { Id = 6, Name = "Elegant Essence Boutique" });

        existingVendor!.Name = "Horizontal Vertical Water";
        await _dbContext.SaveChangesAsync();
        var updatedVendor = await _dbContext.Vendors.FirstOrDefaultAsync(x => x.Id == 6);

        updatedVendor.Should().NotBeNull().And.BeEquivalentTo(new { Id = 6, Name = "Horizontal Vertical Water" });
    }

    [Fact]
    public async Task ShouldDeleteVendor()
    {
        var existingVendor = await _dbContext.Vendors.FirstOrDefaultAsync(x => x.Id == 7);

        existingVendor.Should().NotBeNull().And.BeEquivalentTo(new { Id = 7, Name = "Caffeine Haven Roasters" });

        _dbContext.Remove(existingVendor!);
        await _dbContext.SaveChangesAsync();
        var deletedVendor = await _dbContext.Vendors.FirstOrDefaultAsync(x => x.Id == 7);

        deletedVendor.Should().BeNull();
    }

    [Fact]
    public async Task ShouldNotTrackVendor()
    {
        var existingVendor = await _dbContext.Vendors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 8);

        existingVendor.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Fitness Fusion Studio" });

        existingVendor!.Name = "Horizontal Vertical Water";
        await _dbContext.SaveChangesAsync();
        var updatedVendor = await _dbContext.Vendors.FirstOrDefaultAsync(x => x.Id == 8);

        updatedVendor.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Fitness Fusion Studio" });
    }

    [Fact]
    public async Task ShouldNotTrackVendorWithAsNoTrackAttribute()
    {
        var existingVendor = await _dbContext.QueryableVendorsAsNoTracking.FirstOrDefaultAsync(x => x.Id == 8);

        existingVendor.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Fitness Fusion Studio" });

        existingVendor!.Name = "Horizontal Vertical Water";
        await _dbContext.SaveChangesAsync();
        var updatedVendor = await _dbContext.Vendors.FirstOrDefaultAsync(x => x.Id == 8);

        updatedVendor.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Fitness Fusion Studio" });
    }
}
