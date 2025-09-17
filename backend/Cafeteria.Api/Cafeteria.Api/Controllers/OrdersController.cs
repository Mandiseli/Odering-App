using Cafeteria.Api.Dtos;
using Cafeteria.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _svc;
        public OrdersController(IOrderService svc) => _svc = svc;

        [HttpPost]
        public async Task<IActionResult> Place([FromBody] PlaceOrderRequest req) =>
            Ok(await _svc.PlaceOrderAsync(req.EmployeeNumber, req.Items));

        [HttpGet("employee/{employeeNumber}")]
        public async Task<IActionResult> ForEmployee(string employeeNumber) =>
            Ok(await _svc.GetOrdersForEmployeeAsync(employeeNumber));
    }
}
