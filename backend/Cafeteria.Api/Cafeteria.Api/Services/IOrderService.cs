using Cafeteria.Api.Dtos;
using Cafeteria.Api.Models;

namespace Cafeteria.Api.Services
{
    public interface IOrderService
    {
        Task<Order> PlaceOrderAsync(string employeeNumber, List<OrderItemRequest> items);
        Task<List<Order>> GetOrdersForEmployeeAsync(string employeeNumber);
        Task<List<Order>> GetAllPendingAsync();
        Task<Order> UpdateStatusAsync(int orderId, OrderStatus status);
    }
}
