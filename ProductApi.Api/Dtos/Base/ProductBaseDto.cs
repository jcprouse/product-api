using System.ComponentModel.DataAnnotations;

namespace ProductApi.Api.Dtos.Base
{
    public class ProductBaseDto
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }
    }
}