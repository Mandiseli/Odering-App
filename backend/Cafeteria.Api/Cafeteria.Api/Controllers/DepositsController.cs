using Cafeteria.Api.Dtos;
using Cafeteria.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepositsController : ControllerBase
    {
        private readonly IDepositService _svc;
        public DepositsController(IDepositService svc) => _svc = svc;

        [HttpPost]
        public async Task<IActionResult> MakeDeposit([FromBody] DepositRequest req)
        {
            var res = await _svc.DepositAsync(req.EmployeeNumber, req.DepositAmount);
            return Ok(res);
        }
    }
}
