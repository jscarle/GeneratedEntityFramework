using FluentAssertions;
using GeneratedEntityFramework.Tests.v6.Common;
using GeneratedEntityFramework.Tests.v6.Fixtures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GeneratedEntityFramework.Tests.v6;

public sealed class DependencyInjectionTests(TestContainer container) : TestBase, IAsyncLifetime
{
    private static readonly AsyncLock Mutex = new();
    private static IServiceProvider _serviceProvider = default!;
    private static bool _seeded;
    private IServiceScope _scope = default!;

    public async Task InitializeAsync()
    {
        using (await Mutex.LockAsync())
        {
            if (!_seeded)
            {
                var services = new ServiceCollection();
                services.AddDbContext<DependencyInjectionDbContext>(options => options.UseSqlServer(container.ConnectionString));
                services.AddDbContextInterfaces();
                _serviceProvider = services.BuildServiceProvider();
            }

            _scope = _serviceProvider.CreateScope();

            if (!_seeded)
            {
                var dbContext = _scope.ServiceProvider.GetRequiredService<DependencyInjectionDbContext>();

                await dbContext.Database.EnsureCreatedAsync();

                dbContext.AddRange(GetCustomersSeedData());
                dbContext.AddRange(GetVendorsSeedData());
                await dbContext.SaveChangesAsync();
            }

            _seeded = true;
        }
    }

    public Task DisposeAsync()
    {
        _scope.Dispose();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task ShouldSeedCustomer()
    {
        var customersInterface = _scope.ServiceProvider.GetRequiredService<IDependencyInjectionDbContextCustomers>();

        var seededCustomers = await customersInterface.Customers.Where(x => x.Id >= 1 && x.Id <= 5).ToListAsync();

        seededCustomers.Count.Should().Be(5);
    }

    [Fact]
    public async Task ShouldSaveCustomer()
    {
        var customersInterface = _scope.ServiceProvider.GetRequiredService<IDependencyInjectionDbContextCustomers>();

        customersInterface.Add(new Customer { Id = 42, Name = "Douglas Adams" });
        await customersInterface.SaveChangesAsync();
        var customer = await customersInterface.Customers.FirstOrDefaultAsync(x => x.Id == 42);

        customer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 42, Name = "Douglas Adams" });
    }

    [Fact]
    public async Task ShouldUpdateCustomer()
    {
        var customersInterface = _scope.ServiceProvider.GetRequiredService<IDependencyInjectionDbContextCustomers>();

        var existingCustomer = await customersInterface.Customers.FirstOrDefaultAsync(x => x.Id == 6);

        existingCustomer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 6, Name = "Isabella Khan" });

        existingCustomer!.Name = "Jennifer Moris";
        await customersInterface.SaveChangesAsync();
        var updatedCustomer = await customersInterface.Customers.FirstOrDefaultAsync(x => x.Id == 6);

        updatedCustomer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 6, Name = "Jennifer Moris" });
    }

    [Fact]
    public async Task ShouldDeleteCustomer()
    {
        var customersInterface = _scope.ServiceProvider.GetRequiredService<IDependencyInjectionDbContextCustomers>();

        var existingCustomer = await customersInterface.Customers.FirstOrDefaultAsync(x => x.Id == 7);

        existingCustomer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 7, Name = "Lucas Singh" });

        customersInterface.Remove(existingCustomer!);
        await customersInterface.SaveChangesAsync();
        var deletedCustomer = await customersInterface.Customers.FirstOrDefaultAsync(x => x.Id == 7);

        deletedCustomer.Should().BeNull();
    }

    [Fact]
    public async Task ShouldNotTrackCustomer()
    {
        var customersInterface = _scope.ServiceProvider.GetRequiredService<IDependencyInjectionDbContextCustomers>();

        var existingCustomer = await customersInterface.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 8);

        existingCustomer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Sophia Wang" });

        existingCustomer!.Name = "Jennifer Moris";
        await customersInterface.SaveChangesAsync();
        var updatedCustomer = await customersInterface.Customers.FirstOrDefaultAsync(x => x.Id == 8);

        updatedCustomer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Sophia Wang" });
    }

    [Fact]
    public async Task ShouldNotTrackCustomerWithAsNoTrackAttribute()
    {
        var customersInterface = _scope.ServiceProvider.GetRequiredService<IDependencyInjectionDbContextCustomers>();

        var existingCustomer = await customersInterface.DbSetCustomersAsNoTracking.FirstOrDefaultAsync(x => x.Id == 8);

        existingCustomer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Sophia Wang" });

        existingCustomer!.Name = "Jennifer Moris";
        await customersInterface.SaveChangesAsync();
        var updatedCustomer = await customersInterface.Customers.FirstOrDefaultAsync(x => x.Id == 8);

        updatedCustomer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Sophia Wang" });
    }

    [Fact]
    public async Task ShouldSeedVendor()
    {
        var vendorsInterface = _scope.ServiceProvider.GetRequiredService<IDependencyInjectionDbContextVendors>();

        var seededVendors = await vendorsInterface.Vendors.Where(x => x.Id >= 1 && x.Id <= 5).ToListAsync();

        seededVendors.Count.Should().Be(5);
    }

    [Fact]
    public async Task ShouldSaveVendor()
    {
        var vendorsInterface = _scope.ServiceProvider.GetRequiredService<IDependencyInjectionDbContextVendors>();

        vendorsInterface.Add(new Vendor { Id = 42, Name = "Galaxy Dreams" });
        await vendorsInterface.SaveChangesAsync();
        var customer = await vendorsInterface.Vendors.FirstOrDefaultAsync(x => x.Id == 42);

        customer.Should().NotBeNull().And.BeEquivalentTo(new { Id = 42, Name = "Galaxy Dreams" });
    }

    [Fact]
    public async Task ShouldUpdateVendor()
    {
        var vendorsInterface = _scope.ServiceProvider.GetRequiredService<IDependencyInjectionDbContextVendors>();

        var existingVendor = await vendorsInterface.Vendors.FirstOrDefaultAsync(x => x.Id == 6);

        existingVendor.Should().NotBeNull().And.BeEquivalentTo(new { Id = 6, Name = "Elegant Essence Boutique" });

        existingVendor!.Name = "Horizontal Vertical Water";
        await vendorsInterface.SaveChangesAsync();
        var updatedVendor = await vendorsInterface.Vendors.FirstOrDefaultAsync(x => x.Id == 6);

        updatedVendor.Should().NotBeNull().And.BeEquivalentTo(new { Id = 6, Name = "Horizontal Vertical Water" });
    }

    [Fact]
    public async Task ShouldDeleteVendor()
    {
        var vendorsInterface = _scope.ServiceProvider.GetRequiredService<IDependencyInjectionDbContextVendors>();

        var existingVendor = await vendorsInterface.Vendors.FirstOrDefaultAsync(x => x.Id == 7);

        existingVendor.Should().NotBeNull().And.BeEquivalentTo(new { Id = 7, Name = "Caffeine Haven Roasters" });

        vendorsInterface.Remove(existingVendor!);
        await vendorsInterface.SaveChangesAsync();
        var deletedVendor = await vendorsInterface.Vendors.FirstOrDefaultAsync(x => x.Id == 7);

        deletedVendor.Should().BeNull();
    }

    [Fact]
    public async Task ShouldNotTrackVendor()
    {
        var vendorsInterface = _scope.ServiceProvider.GetRequiredService<IDependencyInjectionDbContextVendors>();

        var existingVendor = await vendorsInterface.Vendors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 8);

        existingVendor.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Fitness Fusion Studio" });

        existingVendor!.Name = "Horizontal Vertical Water";
        await vendorsInterface.SaveChangesAsync();
        var updatedVendor = await vendorsInterface.Vendors.FirstOrDefaultAsync(x => x.Id == 8);

        updatedVendor.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Fitness Fusion Studio" });
    }

    [Fact]
    public async Task ShouldNotTrackVendorWithAsNoTrackAttribute()
    {
        var vendorsInterface = _scope.ServiceProvider.GetRequiredService<IDependencyInjectionDbContextVendors>();

        var existingVendor = await vendorsInterface.QueryableVendorsAsNoTracking.FirstOrDefaultAsync(x => x.Id == 8);

        existingVendor.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Fitness Fusion Studio" });

        existingVendor!.Name = "Horizontal Vertical Water";
        await vendorsInterface.SaveChangesAsync();
        var updatedVendor = await vendorsInterface.Vendors.FirstOrDefaultAsync(x => x.Id == 8);

        updatedVendor.Should().NotBeNull().And.BeEquivalentTo(new { Id = 8, Name = "Fitness Fusion Studio" });
    }
}
