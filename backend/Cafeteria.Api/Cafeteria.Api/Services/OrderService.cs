using Cafeteria.Api.Data;
using Cafeteria.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Api.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> PlaceOrderAsync(string employeeNumber, Dictionary<int, int> items)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeNumber == employeeNumber);
        if (employee == null) return null;

        var menuItems = await _context.MenuItems.Where(m => items.Keys.Contains(m.Id)).ToListAsync();
        if (!menuItems.Any()) return null;

        decimal total = 0;
        var order = new Order { EmployeeId = employee.Id };

        foreach (var menuItem in menuItems)
        {
            int qty = items[menuItem.Id];
            decimal lineTotal = qty * menuItem.Price;
            total += lineTotal;

            order.Items.Add(new OrderItem
            {
                MenuItemId = menuItem.Id,
                Quantity = qty,
                UnitPriceAtTimeOfOrder = menuItem.Price
            });
        }

        if (employee.Balance < total) return null;

        employee.Balance -= total;
        order.TotalAmount = total;

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<List<Order>> GetOrdersForEmployeeAsync(string employeeNumber)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeNumber == employeeNumber);
        if (employee == null) return new List<Order>();

        return await _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.MenuItem)
            .Where(o => o.EmployeeId == employee.Id)
            .ToListAsync();
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders.Include(o => o.Employee).Include(o => o.Items).ToListAsync();
    }

    public async Task<Order?> UpdateOrderStatusAsync(int orderId, string status)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null) return null;

        order.Status = status;
        await _context.SaveChangesAsync();

        return order;
    }
}
