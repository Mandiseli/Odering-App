namespace Cafeteria.Api.Dtos
{
    public record DepositRequest(string EmployeeNumber, decimal DepositAmount);
    public record DepositResponse(int EmployeeId, decimal DepositAmount, decimal BonusApplied, decimal NewBalance);
}
