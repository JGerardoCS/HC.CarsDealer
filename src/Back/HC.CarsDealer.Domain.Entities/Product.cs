using System;

namespace HC.CarsDealer.Domain.Entities
{
    public class Product : BaseEntity {
        public string Model { get; set; }
        public string Description { get; set; }
        public int? Year { get; set; }
        public string Brand { get; set; }
        public int Kilometers { get; set; }
        public decimal Price { get; set; }

        public Product() {
            Id = 0;
            Model = string.Empty;
            Description = string.Empty;
            Year = 0;
            Brand = string.Empty;
            Kilometers = 0;
            Price = 0;
        }
    }
}
