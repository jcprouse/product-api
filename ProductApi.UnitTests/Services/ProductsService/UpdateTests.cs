using System;
using System.Collections.Generic;
using Moq.EntityFrameworkCore;
using ProductApi.Api.Dtos.Put;
using ProductApi.Api.Exceptions;
using ProductApi.Data.Models;
using Xunit;

namespace ProductApi.UnitTests.Services.ProductsService
{
    public class UpdateTests : ProductsServiceTests
    {
        private readonly List<Product> _productData;

        private readonly Guid _productId;

        public UpdateTests()
        {
            // arrange
            _productId = Guid.NewGuid();

            _productData = new List<Product>
            {
                new Product
                {
                    Id = 1, UniqueId = _productId
                }
            };

            MockDbContext.Setup(x => x.Products).ReturnsDbSet(_productData);
        }

        [Fact]
        private void should_update_the_product_in_the_database()
        {
            // arrange
            var productDto = new ProductDto
            {
                DeliveryPrice = new decimal(1.1),
                Description = "desc",
                Name = "name",
                Price = new decimal(2.2)
            };

            // act
            ProductsService.Update(_productId, productDto);

            // assert
            Assert.Single(_productData);
            Assert.Equal(_productId, _productData[0].UniqueId);
            Assert.Equal(productDto.Description, _productData[0].Description);
            Assert.Equal(productDto.DeliveryPrice, _productData[0].DeliveryPrice);
            Assert.Equal(productDto.Name, _productData[0].Name);
            Assert.Equal(productDto.Price, _productData[0].Price);
        }

        [Fact]
        private void should_return_404_if_supplied_product_id_does_not_exist()
        {
            // act / assert
            Assert.Throws<NotFoundException>(() =>
                ProductsService.Update(new Guid("00000000-0000-0000-0000-000000000000"), new ProductDto()));
        }
    }
}