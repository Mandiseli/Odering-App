using Cafeteria.Api.Data;
using Cafeteria.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Api.Services
{
    public class DepositService : IDepositService
    {
        private readonly ApplicationDbContext _db;
        private const decimal Threshold = 250m;
        private const decimal BonusPerThreshold = 500m;

        public DepositService(ApplicationDbContext db) => _db = db;

        public async Task<DepositResponse> DepositAsync(string employeeNumber, decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Deposit amount must be positive.");

            var emp = await _db.Employees.SingleOrDefaultAsync(e => e.EmployeeNumber == employeeNumber)
                ?? throw new InvalidOperationException("Employee not found.");

            var now = DateTime.UtcNow;
            var currentMonth = new DateTime(now.Year, now.Month, 1);

            if (emp.LastDepositMonth == null ||
                emp.LastDepositMonth.Value.Year != now.Year ||
                emp.LastDepositMonth.Value.Month != now.Month)
            {
                emp.LastDepositMonth = currentMonth;
                emp.MonthlyDepositTotal = 0m;
            }

            var prev = emp.MonthlyDepositTotal;
            emp.MonthlyDepositTotal += amount;

            // steps gained this deposit only
            var prevSteps = (int)Math.Floor(prev / Threshold);
            var newSteps = (int)Math.Floor(emp.MonthlyDepositTotal / Threshold);
            var stepsGained = Math.Max(0, newSteps - prevSteps);
            var bonus = stepsGained * BonusPerThreshold;

            emp.Balance += amount + bonus;

            _db.Deposits.Add(new Models.Deposit
            {
                EmployeeId = emp.Id,
                Amount = amount,
                BonusApplied = bonus,
                CreatedAt = now
            });

            await _db.SaveChangesAsync();

            return new DepositResponse(emp.Id, amount, bonus, emp.Balance);
        }
    }
}
