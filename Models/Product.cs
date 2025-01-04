using System.ComponentModel.DataAnnotations;

namespace ProductDB.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}
