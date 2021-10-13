using System;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProductApi.Api.Dtos.Post;
using ProductApi.Data.Models;
using Xunit;

namespace ProductApi.UnitTests.Services.ProductsService
{
    public class CreateTests : ProductsServiceTests
    {
        private readonly Mock<DbSet<Product>> _productData;

        private readonly ProductDto _productDto;

        public CreateTests()
        {
            // arrange
            _productData = new Mock<DbSet<Product>>();
            MockDbContext.Setup(x => x.Products).Returns(_productData.Object);

            _productDto = new ProductDto
            {
                DeliveryPrice = new decimal(1.1),
                Description = "desc",
                Name = "name",
                Price = new decimal(2.2)
            };
        }

        [Fact]
        private void should_add_the_product_to_the_database()
        {
            // arrange
            Product capturedProduct = null;

            _productData.Setup(x => x.Add(It.IsAny<Product>())).Callback((Product p) => capturedProduct = p);

            // act
            ProductsService.Create(_productDto);

            // assert
            Assert.Equal(_productDto.Description, capturedProduct.Description);
            Assert.Equal(_productDto.DeliveryPrice, capturedProduct.DeliveryPrice);
            Assert.Equal(_productDto.Name, capturedProduct.Name);
            Assert.Equal(_productDto.Price, capturedProduct.Price);
        }

        [Fact]
        private void should_return_a_product_dto_with_the_id()
        {
            // arrange
            var overridenNewId = Guid.NewGuid();

            _productData.Setup(x => x.Add(It.IsAny<Product>()))
                .Callback((Product p) => { p.UniqueId = overridenNewId; });

            // act
            var response = ProductsService.Create(_productDto);

            // assert
            Assert.Equal(overridenNewId, response.Id);
            Assert.Equal(_productDto.Description, response.Description);
            Assert.Equal(_productDto.DeliveryPrice, response.DeliveryPrice);
            Assert.Equal(_productDto.Name, response.Name);
            Assert.Equal(_productDto.Price, response.Price);
        }
    }
}