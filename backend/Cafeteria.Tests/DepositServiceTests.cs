using Cafeteria.Api.Data;
using Cafeteria.Api.Models;
using Cafeteria.Api.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;

namespace Cafeteria.Tests;

public class DepositServiceTests
{
    private async Task<ApplicationDbContext> GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ApplicationDbContext(options);
        context.Employees.Add(new Employee { Name = "Sipho", EmployeeNumber = "EMP001", Balance = 0 });
        await context.SaveChangesAsync();
        return context;
    }

    [Fact]
    public async Task Deposit_ShouldAddBonus_When250Deposited()
    {
        var context = await GetDbContext();
        var service = new DepositService(context);

        var result = await service.MakeDepositAsync("EMP001", 250);

        result!.Balance.Should().Be(750);
    }
}
