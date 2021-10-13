using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using ProductApi.Api.Dtos.Post;
using ProductApi.Api.Exceptions;
using ProductApi.Data.Models;
using Xunit;

namespace ProductApi.UnitTests.Services.ProductOptionsService
{
    public class CreateTests : ProductOptionsServiceTests
    {
        private readonly Guid _productId;
        private readonly Mock<DbSet<ProductOption>> _productOptionData;

        private readonly ProductOptionDto _productOptionDto;

        public CreateTests()
        {
            // arrange
            _productOptionData = new Mock<DbSet<ProductOption>>();
            MockDbContext.Setup(x => x.ProductOptions).Returns(_productOptionData.Object);

            _productId = Guid.NewGuid();

            var productData = new List<Product>
            {
                new Product
                {
                    Id = 1, UniqueId = _productId
                }
            };
            MockDbContext.Setup(x => x.Products).ReturnsDbSet(productData);

            _productOptionDto = new ProductOptionDto
            {
                Description = "desc",
                Name = "name"
            };
        }

        [Fact]
        private void should_add_the_product_option_to_the_database()
        {
            // arrange
            ProductOption capturedProductOption = null;

            _productOptionData.Setup(x => x.Add(It.IsAny<ProductOption>()))
                .Callback<ProductOption>(p => capturedProductOption = p);

            // act
            var response = ProductOptionsService.Create(_productId, _productOptionDto);

            // assert
            Assert.Equal(_productOptionDto.Description, capturedProductOption.Description);
            Assert.Equal(_productOptionDto.Name, capturedProductOption.Name);
            Assert.Equal(_productId, capturedProductOption.ProductId);
        }

        [Fact]
        private void should_return_a_product_option_dto_with_the_new_id()
        {
            // arrange
            var overridenNewId = Guid.NewGuid();

            _productOptionData.Setup(x => x.Add(It.IsAny<ProductOption>()))
                .Callback((ProductOption p) => p.UniqueId = overridenNewId);

            // act
            var response = ProductOptionsService.Create(_productId, _productOptionDto);

            // assert
            Assert.Equal(overridenNewId, response.Id);
            Assert.Equal(_productOptionDto.Description, response.Description);
            Assert.Equal(_productOptionDto.Name, response.Name);
        }

        [Fact]
        private void should_return_404_if_supplied_product_id_does_not_exist()
        {
            // act / assert
            Assert.Throws<NotFoundException>(() =>
                ProductOptionsService.Create(new Guid("00000000-0000-0000-0000-000000000000"), new ProductOptionDto()));
        }
    }
}