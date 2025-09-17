using Cafeteria.Api.Dtos;

namespace Cafeteria.Api.Services
{
    public interface IDepositService
    {
        Task<DepositResponse> DepositAsync(string employeeNumber, decimal amount);
    }
}
