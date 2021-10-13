using System;
using ProductApi.Api.Dtos.Post;
using ProductApi.Data.Models;

namespace ProductApi.Api.Converters
{
    public static class ProductConverter
    {
        public static Product ConvertToModel(ProductDto productDto)
        {
            return new Product
            {
                DeliveryPrice = productDto.DeliveryPrice, Description = productDto.Description, Name = productDto.Name,
                Price = productDto.Price
            };
        }

        public static Product ConvertToModel(Dtos.Put.ProductDto productDto, Guid uniqueId)
        {
            return new Product
            {
                DeliveryPrice = productDto.DeliveryPrice, Description = productDto.Description, Name = productDto.Name,
                Price = productDto.Price, UniqueId = uniqueId
            };
        }

        public static Dtos.Get.ProductDto ConvertToDto(Product product)
        {
            return new Dtos.Get.ProductDto
            {
                DeliveryPrice = product.DeliveryPrice,
                Description = product.Description,
                Id = product.UniqueId,
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}