using Microsoft.EntityFrameworkCore;

namespace GeneratedEntityFramework.Tests.v3.Common;

public class TestBase<TDbContext> : IClassFixture<TestContainer<TDbContext>>
    where TDbContext : DbContext
{
    protected IEnumerable<Customer> GetCustomersSeedData()
    {
        return new[]
        {
            new Customer { Id = 1, Name = "Liam Patel" },
            new Customer { Id = 2, Name = "Ava Nguyen" },
            new Customer { Id = 3, Name = "Ethan Garcia" },
            new Customer { Id = 4, Name = "Mia Gonzalez" },
            new Customer { Id = 5, Name = "Noah Kim" },
            new Customer { Id = 6, Name = "Isabella Khan" },
            new Customer { Id = 7, Name = "Lucas Singh" },
            new Customer { Id = 8, Name = "Sophia Wang" },
            new Customer { Id = 9, Name = "Oliver Das" },
            new Customer { Id = 10, Name = "Sophia Taylor" },
        };
    }

    protected IEnumerable<Vendor> GetVendorsSeedData()
    {
        return new[]
        {
            new Vendor { Id = 1, Name = "LuxTech Solutions" },
            new Vendor { Id = 2, Name = "Bella's Bistro" },
            new Vendor { Id = 3, Name = "Alpha Automotive Services" },
            new Vendor { Id = 4, Name = "Garden Grace Landscaping" },
            new Vendor { Id = 5, Name = "PetPamper Spa" },
            new Vendor { Id = 6, Name = "Elegant Essence Boutique" },
            new Vendor { Id = 7, Name = "Caffeine Haven Roasters" },
            new Vendor { Id = 8, Name = "Fitness Fusion Studio" },
            new Vendor { Id = 9, Name = "Wilderness Wanderlust Tours" },
            new Vendor { Id = 10, Name = "Envision Interiors" },
        };
    }
}
