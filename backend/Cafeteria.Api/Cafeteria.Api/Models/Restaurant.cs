using System.ComponentModel.DataAnnotations;

namespace Cafeteria.Api.Models
{
    public class Restaurant
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? LocationDescription { get; set; }

        [MaxLength(30)]
        public string? ContactNumber { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}
