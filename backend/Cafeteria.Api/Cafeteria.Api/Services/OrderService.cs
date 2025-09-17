using Cafeteria.Api.Data;
using Cafeteria.Api.Dtos;
using Cafeteria.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _db;
        public OrderService(ApplicationDbContext db) => _db = db;

        public async Task<Order> PlaceOrderAsync(string employeeNumber, List<OrderItemRequest> items)
        {
            if (items is null || items.Count == 0) throw new ArgumentException("No items in order.");

            var emp = await _db.Employees.SingleOrDefaultAsync(e => e.EmployeeNumber == employeeNumber)
                ?? throw new InvalidOperationException("Employee not found.");

            var ids = items.Select(i => i.MenuItemId).ToList();
            var menu = await _db.MenuItems.Where(m => ids.Contains(m.Id)).ToDictionaryAsync(m => m.Id);

            decimal total = 0m;
            var orderItems = new List<OrderItem>();

            foreach (var req in items)
            {
                if (!menu.TryGetValue(req.MenuItemId, out var mi))
                    throw new InvalidOperationException($"Menu item {req.MenuItemId} not found.");
                if (req.Quantity <= 0) throw new ArgumentException("Quantity must be positive.");

                total += mi.Price * req.Quantity;
                orderItems.Add(new OrderItem
                {
                    MenuItemId = req.MenuItemId,
                    Quantity = req.Quantity,
                    UnitPriceAtTimeOfOrder = mi.Price
                });
            }

            if (emp.Balance < total)
                throw new InvalidOperationException("Insufficient balance.");

            using var tx = await _db.Database.BeginTransactionAsync();

            emp.Balance -= total;
            var order = new Order
            {
                EmployeeId = emp.Id,
                OrderDate = DateTime.UtcNow,
                TotalAmount = total,
                Status = OrderStatus.Pending,
                Items = orderItems
            };
            _db.Orders.Add(order);

            await _db.SaveChangesAsync();
            await tx.CommitAsync();

            return order;
        }

        public Task<List<Order>> GetOrdersForEmployeeAsync(string employeeNumber) =>
            _db.Orders.Include(o => o.Items)
                      .Include(o => o.Employee)
                      .Where(o => o.Employee!.EmployeeNumber == employeeNumber)
                      .OrderByDescending(o => o.OrderDate)
                      .ToListAsync();

        public Task<List<Order>> GetAllPendingAsync() =>
            _db.Orders.Include(o => o.Employee)
                      .Where(o => o.Status != OrderStatus.Delivered)
                      .OrderBy(o => o.OrderDate)
                      .ToListAsync();

        public async Task<Order> UpdateStatusAsync(int orderId, OrderStatus status)
        {
            var order = await _db.Orders.FindAsync(orderId)
                ?? throw new InvalidOperationException("Order not found.");
            order.Status = status;
            await _db.SaveChangesAsync();
            return order;
        }
    }
}
