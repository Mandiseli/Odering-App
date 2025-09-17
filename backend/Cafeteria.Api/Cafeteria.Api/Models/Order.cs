using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Api.Models
{
    public enum OrderStatus { Pending, Preparing, Delivering, Delivered }

    public class Order
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
