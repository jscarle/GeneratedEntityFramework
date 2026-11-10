using FluentAssertions;
using GeneratedEntityFramework.Tests.Integration.Common;
using GeneratedEntityFramework.Tests.Integration.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace GeneratedEntityFramework.Tests.Integration;

public sealed class InterfaceMappedTests(TestContainer container) : TestBase, IAsyncLifetime
{
    private static readonly SemaphoreSlim Mutex = new(1, 1);
    private static bool _seeded;
    private static InterfaceMappedDbContext _dbContext = default!;

    public async ValueTask InitializeAsync()
    {
        await Mutex.WaitAsync(TestContext.Current.CancellationToken);

        try
        {
            if (_seeded)
                return;

            var builder = new DbContextOptionsBuilder<InterfaceMappedDbContext>();
            builder.UseSqlServer(container.ConnectionString);
            _dbContext = new InterfaceMappedDbContext(builder.Options);

            await _dbContext.Database.EnsureCreatedAsync(TestContext.Current.CancellationToken);

            _dbContext.AddRange(GetCustomersSeedData());
            _dbContext.AddRange(GetVendorsSeedData());
            await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

            _seeded = true;
        }
        finally
        {
            Mutex.Release();
        }
    }

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }

    [Fact]
    public async Task ShouldSeedCustomer()
    {
        var seededCustomers = await _dbContext.Customers.Where(x => x.Id >= 1 && x.Id <= 5).ToListAsync(TestContext.Current.CancellationToken);

        seededCustomers.Count.Should().Be(5);
    }

    [Fact]
    public async Task ShouldSaveCustomer()
    {
        _dbContext.Add(new Customer { Id = 42, Name = "Douglas Adams" });
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);
        var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == 42, TestContext.Current.CancellationToken);

        customer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 42, Name = "Douglas Adams" });
    }

    [Fact]
    public async Task ShouldUpdateCustomer()
    {
        var existingCustomer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == 6, TestContext.Current.CancellationToken);

        existingCustomer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 6, Name = "Isabella Khan" });

        existingCustomer.Name = "Jennifer Moris";
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);
        var updatedCustomer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == 6, TestContext.Current.CancellationToken);

        updatedCustomer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 6, Name = "Jennifer Moris" });
    }

    [Fact]
    public async Task ShouldDeleteCustomer()
    {
        var existingCustomer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == 7, TestContext.Current.CancellationToken);

        existingCustomer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 7, Name = "Lucas Singh" });

        _dbContext.Remove(existingCustomer);
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);
        var deletedCustomer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == 7, TestContext.Current.CancellationToken);

        deletedCustomer.Should().BeNull();
    }

    [Fact]
    public async Task ShouldNotTrackCustomer()
    {
        var existingCustomer = await _dbContext.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 8, TestContext.Current.CancellationToken);

        existingCustomer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Sophia Wang" });

        existingCustomer.Name = "Jennifer Moris";
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);
        var updatedCustomer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == 8, TestContext.Current.CancellationToken);

        updatedCustomer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Sophia Wang" });
    }

    [Fact]
    public async Task ShouldNotTrackCustomerWithAsNoTrackAttribute()
    {
        var existingCustomer = await _dbContext.DbSetCustomersAsNoTracking.FirstOrDefaultAsync(x => x.Id == 8, TestContext.Current.CancellationToken);

        existingCustomer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Sophia Wang" });

        existingCustomer.Name = "Jennifer Moris";
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);
        var updatedCustomer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == 8, TestContext.Current.CancellationToken);

        updatedCustomer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Sophia Wang" });
    }

    [Fact]
    public async Task ShouldSeedVendor()
    {
        var seededVendors = await _dbContext.Vendors.Where(x => x.Id >= 1 && x.Id <= 5).ToListAsync(TestContext.Current.CancellationToken);

        seededVendors.Count.Should().Be(5);
    }

    [Fact]
    public async Task ShouldSaveVendor()
    {
        _dbContext.Add(new Vendor { Id = 42, Name = "Galaxy Dreams" });
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);
        var customer = await _dbContext.Vendors.FirstOrDefaultAsync(x => x.Id == 42, TestContext.Current.CancellationToken);

        customer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 42, Name = "Galaxy Dreams" });
    }

    [Fact]
    public async Task ShouldUpdateVendor()
    {
        var existingVendor = await _dbContext.Vendors.FirstOrDefaultAsync(x => x.Id == 6, TestContext.Current.CancellationToken);

        existingVendor.Should().NotBeNull().And.BeEquivalentTo(new { Id = 6, Name = "Elegant Essence Boutique" });

        existingVendor.Name = "Horizontal Vertical Water";
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);
        var updatedVendor = await _dbContext.Vendors.FirstOrDefaultAsync(x => x.Id == 6, TestContext.Current.CancellationToken);

        updatedVendor.Should().NotBeNull().And.BeEquivalentTo(new { Id = 6, Name = "Horizontal Vertical Water" });
    }

    [Fact]
    public async Task ShouldDeleteVendor()
    {
        var existingVendor = await _dbContext.Vendors.FirstOrDefaultAsync(x => x.Id == 7, TestContext.Current.CancellationToken);

        existingVendor.Should().NotBeNull().And.BeEquivalentTo(new { Id = 7, Name = "Caffeine Haven Roasters" });

        _dbContext.Remove(existingVendor);
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);
        var deletedVendor = await _dbContext.Vendors.FirstOrDefaultAsync(x => x.Id == 7, TestContext.Current.CancellationToken);

        deletedVendor.Should().BeNull();
    }

    [Fact]
    public async Task ShouldNotTrackVendor()
    {
        var existingVendor = await _dbContext.Vendors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 8, TestContext.Current.CancellationToken);

        existingVendor.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Fitness Fusion Studio" });

        existingVendor.Name = "Horizontal Vertical Water";
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);
        var updatedVendor = await _dbContext.Vendors.FirstOrDefaultAsync(x => x.Id == 8, TestContext.Current.CancellationToken);

        updatedVendor.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Fitness Fusion Studio" });
    }

    [Fact]
    public async Task ShouldNotTrackVendorWithAsNoTrackAttribute()
    {
        var existingVendor = await _dbContext.QueryableVendorsAsNoTracking.FirstOrDefaultAsync(x => x.Id == 8, TestContext.Current.CancellationToken);

        existingVendor.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Fitness Fusion Studio" });

        existingVendor.Name = "Horizontal Vertical Water";
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);
        var updatedVendor = await _dbContext.Vendors.FirstOrDefaultAsync(x => x.Id == 8, TestContext.Current.CancellationToken);

        updatedVendor.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Fitness Fusion Studio" });
    }
}
