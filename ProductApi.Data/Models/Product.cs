using System;

namespace ProductApi.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        public Guid UniqueId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }
    }
}