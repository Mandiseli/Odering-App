using Cafeteria.Api.Models;

namespace Cafeteria.Api.Services;

public interface IOrderService
{
    Task<Order?> PlaceOrderAsync(string employeeNumber, Dictionary<int, int> items);
    Task<List<Order>> GetOrdersForEmployeeAsync(string employeeNumber);
    Task<List<Order>> GetAllOrdersAsync();
    Task<Order?> UpdateOrderStatusAsync(int orderId, string status);
}
