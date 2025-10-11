using Cafeteria.Api.Models;
using Cafeteria.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("place")]
    public async Task<ActionResult<Order>> PlaceOrder(string employeeNumber, Dictionary<int, int> items)
    {
        var order = await _orderService.PlaceOrderAsync(employeeNumber, items);
        if (order == null) return BadRequest("Order failed.");
        return Ok(order);
    }

    [HttpGet("employee/{employeeNumber}")]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrdersForEmployee(string employeeNumber)
    {
        return await _orderService.GetOrdersForEmployeeAsync(employeeNumber);
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
    {
        return await _orderService.GetAllOrdersAsync();
    }

    [HttpPut("{id}/status")]
    public async Task<ActionResult<Order>> UpdateStatus(int id, string status)
    {
        var order = await _orderService.UpdateOrderStatusAsync(id, status);
        if (order == null) return NotFound();
        return Ok(order);
    }
}
