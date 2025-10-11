using Cafeteria.Api.Data;
using Cafeteria.Api.Models;
using Cafeteria.Api.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;

namespace Cafeteria.Tests;

public class OrderServiceTests
{
    private async Task<ApplicationDbContext> GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ApplicationDbContext(options);

        var emp = new Employee { Name = "Sipho", EmployeeNumber = "EMP001", Balance = 1000 };
        context.Employees.Add(emp);
        context.MenuItems.Add(new MenuItem { Id = 1, Name = "Burger", Price = 100, RestaurantId = 1 });
        await context.SaveChangesAsync();
        return context;
    }

    [Fact]
    public async Task PlaceOrder_ShouldDeductBalance()
    {
        var context = await GetDbContext();
        var service = new OrderService(context);

        var order = await service.PlaceOrderAsync("EMP001", new Dictionary<int, int> { { 1, 2 } });

        order!.TotalAmount.Should().Be(200);
        context.Employees.First().Balance.Should().Be(800);
    }
}
