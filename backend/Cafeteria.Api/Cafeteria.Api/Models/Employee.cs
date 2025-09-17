using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Api.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required, MaxLength(120)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string EmployeeNumber { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        // Track monthly deposits for bonus calculation
        public DateTime? LastDepositMonth { get; set; } // first day of month (UTC)
        [Column(TypeName = "decimal(18,2)")]
        public decimal MonthlyDepositTotal { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Deposit> Deposits { get; set; } = new List<Deposit>();
    }
}
