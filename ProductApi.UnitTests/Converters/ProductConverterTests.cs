using System;
using ProductApi.Api.Converters;
using ProductApi.Api.Dtos.Post;
using ProductApi.Data.Models;
using Xunit;

namespace ProductApi.UnitTests.Converters
{
    public class ProductConverterTests
    {
        public class ConvertToModel : ProductConverterTests
        {
            [Fact]
            private void should_map_a_post_product_dto_to_a_product_model()
            {
                // arrange
                var productDto = new ProductDto
                    { DeliveryPrice = new decimal(1), Description = "desc 1", Name = "name 1", Price = new decimal(2) };

                // act
                var response = ProductConverter.ConvertToModel(productDto);

                // assert
                Assert.Equal(productDto.DeliveryPrice, response.DeliveryPrice);
                Assert.Equal(productDto.Description, response.Description);
                Assert.Equal(productDto.Name, response.Name);
                Assert.Equal(productDto.Price, response.Price);
            }

            [Fact]
            private void should_map_a_put_product_dto_and_guid_to_a_product_model()
            {
                // arrange
                var productDto = new Api.Dtos.Put.ProductDto
                    { DeliveryPrice = new decimal(1), Description = "desc 1", Name = "name 1", Price = new decimal(2) };

                var id = Guid.NewGuid();

                // act
                var response = ProductConverter.ConvertToModel(productDto, id);

                // assert
                Assert.Equal(productDto.DeliveryPrice, response.DeliveryPrice);
                Assert.Equal(productDto.Description, response.Description);
                Assert.Equal(productDto.Name, response.Name);
                Assert.Equal(productDto.Price, response.Price);
                Assert.Equal(id, response.UniqueId);
            }
        }

        public class ConvertToDto : ProductConverterTests
        {
            [Fact]
            private void should_map_a_product_model_to_a_get_product_dto()
            {
                // arrange
                var product = new Product
                {
                    DeliveryPrice = new decimal(1), Description = "desc 1", Name = "name 1", Price = new decimal(2),
                    UniqueId = Guid.NewGuid()
                };

                // act
                var response = ProductConverter.ConvertToDto(product);

                // assert
                Assert.Equal(product.DeliveryPrice, response.DeliveryPrice);
                Assert.Equal(product.Description, response.Description);
                Assert.Equal(product.Name, response.Name);
                Assert.Equal(product.Price, response.Price);
                Assert.Equal(product.UniqueId, response.Id);
            }
        }
    }
}