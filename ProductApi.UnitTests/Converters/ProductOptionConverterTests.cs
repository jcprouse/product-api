using System;
using ProductApi.Api.Converters;
using ProductApi.Api.Dtos.Post;
using ProductApi.Data.Models;
using Xunit;

namespace ProductApi.UnitTests.Converters
{
    public class ProductOptionConverterTests
    {
        public class ConvertToModel : ProductOptionConverterTests
        {
            [Fact]
            private void should_map_a_post_product_option_dto_to_a_product_option_model()
            {
                // arrange
                var productOptionDto = new ProductOptionDto
                    { Description = "desc 1", Name = "name 1" };

                var productId = Guid.NewGuid();

                // act
                var response = ProductOptionConverter.ConvertToModel(productOptionDto, productId);

                // assert
                Assert.Equal(productOptionDto.Description, response.Description);
                Assert.Equal(productOptionDto.Name, response.Name);
                Assert.Equal(productId, response.ProductId);
            }

            [Fact]
            private void should_map_a_put_product_option_dto_and_guid_to_a_product_option_model()
            {
                // arrange
                var productOptionDto = new Api.Dtos.Put.ProductOptionDto
                    { Description = "desc 1", Name = "name 1" };

                var id = Guid.NewGuid();

                var productId = Guid.NewGuid();

                // act
                var response = ProductOptionConverter.ConvertToModel(productOptionDto, id, productId);

                // assert
                Assert.Equal(productOptionDto.Description, response.Description);
                Assert.Equal(productOptionDto.Name, response.Name);
                Assert.Equal(id, response.UniqueId);
                Assert.Equal(productId, response.ProductId);
            }
        }

        public class ConvertToDto : ProductOptionConverterTests
        {
            [Fact]
            private void should_map_a_product_option_model_to_a_get_product_option_dto()
            {
                // arrange
                var productOption = new ProductOption
                {
                    Description = "desc 1", Name = "name 1", UniqueId = Guid.NewGuid()
                };

                // act
                var response = ProductOptionConverter.ConvertToDto(productOption);

                // assert
                Assert.Equal(productOption.Description, response.Description);
                Assert.Equal(productOption.Name, response.Name);
                Assert.Equal(productOption.UniqueId, response.Id);
            }
        }
    }
}