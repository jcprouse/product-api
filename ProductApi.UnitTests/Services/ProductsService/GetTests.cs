using System;
using System.Collections.Generic;
using Moq.EntityFrameworkCore;
using ProductApi.Api.Exceptions;
using ProductApi.Data.Models;
using Xunit;

namespace ProductApi.UnitTests.Services.ProductsService
{
    public class GetTests : ProductsServiceTests
    {
        private readonly List<Product> _productData;

        private readonly Guid _productId;

        public GetTests()
        {
            // arrange
            _productId = Guid.NewGuid();

            _productData = new List<Product>
            {
                new Product
                {
                    Id = 1, UniqueId = Guid.NewGuid()
                },
                new Product
                {
                    Id = 2, UniqueId = _productId, Description = "desc", DeliveryPrice = new decimal(1), Name = "name",
                    Price = new decimal(1.1)
                }
            };

            MockDbContext.Setup(x => x.Products).ReturnsDbSet(_productData);
        }

        [Fact]
        private void should_return_the_product_matching_the_id_from_the_database()
        {
            // act
            var response = ProductsService.Get(_productId);

            // assert
            Assert.Equal(_productData[1].UniqueId, response.Id);
            Assert.Equal(_productData[1].Description, response.Description);
            Assert.Equal(_productData[1].DeliveryPrice, response.DeliveryPrice);
            Assert.Equal(_productData[1].Name, response.Name);
            Assert.Equal(_productData[1].Price, response.Price);
        }

        [Fact]
        private void should_return_404_if_supplied_product_id_does_not_exist()
        {
            // act / assert
            Assert.Throws<NotFoundException>(
                () => ProductsService.Get(new Guid("00000000-0000-0000-0000-000000000000")));
        }
    }
}