using Cafeteria.Api.Models;
using Cafeteria.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepositsController : ControllerBase
{
    private readonly IDepositService _depositService;

    public DepositsController(IDepositService depositService)
    {
        _depositService = depositService;
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> Deposit(string employeeNumber, decimal amount)
    {
        var result = await _depositService.MakeDepositAsync(employeeNumber, amount);
        if (result == null) return BadRequest("Deposit failed.");
        return Ok(result);
    }
}
