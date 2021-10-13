using System;
using ProductApi.Api.Dtos.Post;
using ProductApi.Data.Models;

namespace ProductApi.Api.Converters
{
    public static class ProductOptionConverter
    {
        public static ProductOption ConvertToModel(ProductOptionDto productOptionDto, Guid productId)
        {
            return new ProductOption
            {
                Description = productOptionDto.Description, Name = productOptionDto.Name, ProductId = productId
            };
        }

        public static ProductOption ConvertToModel(Dtos.Put.ProductOptionDto productDto, Guid uniqueId, Guid productId)
        {
            return new ProductOption
            {
                Description = productDto.Description,
                Name = productDto.Name,
                ProductId = productId,
                UniqueId = uniqueId
            };
        }

        public static Dtos.Get.ProductOptionDto ConvertToDto(ProductOption productOption)
        {
            return new Dtos.Get.ProductOptionDto
            {
                Description = productOption.Description,
                Id = productOption.UniqueId,
                Name = productOption.Name
            };
        }
    }
}