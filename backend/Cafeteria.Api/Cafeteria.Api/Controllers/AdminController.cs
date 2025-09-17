using Cafeteria.Api.Models;
using Cafeteria.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IOrderService _svc;
        public AdminController(IOrderService svc) => _svc = svc;

        [HttpGet("orders/pending")]
        public async Task<IActionResult> Pending() => Ok(await _svc.GetAllPendingAsync());

        [HttpPatch("orders/{id:int}/status/{status}")]
        public async Task<IActionResult> UpdateStatus(int id, OrderStatus status) =>
            Ok(await _svc.UpdateStatusAsync(id, status));
    }
}
