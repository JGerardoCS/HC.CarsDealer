using System;
using System.ComponentModel.DataAnnotations;

namespace HC.CarsDealer.Application.Models
{
    public class ProductModel
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Model { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [Range(0, 9_000)]
        public int? Year { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Brand { get; set; }

        [Required]
        [Range(0, 1_000_000)]
        public int Kilometers { get; set; }

        [Required]
        [Range(1, 10_000_000)]
        public decimal Price { get; set; }
    }
}
