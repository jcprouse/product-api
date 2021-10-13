using System;

namespace ProductApi.Data.Models
{
    public class ProductOption
    {
        public int Id { get; set; }

        public Guid UniqueId { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}