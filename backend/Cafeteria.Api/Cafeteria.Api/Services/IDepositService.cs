using Cafeteria.Api.Models;

namespace Cafeteria.Api.Services;

public interface IDepositService
{
    Task<Employee?> MakeDepositAsync(string employeeNumber, decimal amount);
}
