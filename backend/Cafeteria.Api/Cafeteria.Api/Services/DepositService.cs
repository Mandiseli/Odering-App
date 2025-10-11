using Cafeteria.Api.Data;
using Cafeteria.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Api.Services;

public class DepositService : IDepositService
{
    private readonly ApplicationDbContext _context;

    public DepositService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Employee?> MakeDepositAsync(string employeeNumber, decimal amount)
    {
        if (amount <= 0) return null;

        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeNumber == employeeNumber);
        if (employee == null) return null;

        var now = DateTime.UtcNow;
        var monthKey = new DateTime(now.Year, now.Month, 1);

        decimal bonus = 0;
        if (employee.LastDepositMonth != monthKey)
        {
            employee.LastDepositMonth = monthKey;
        }

        // Calculate bonuses
        int eligibleBonuses = (int)(amount / 250);
        bonus = eligibleBonuses * 500;

        employee.Balance += amount + bonus;

        await _context.SaveChangesAsync();
        return employee;
    }
}
