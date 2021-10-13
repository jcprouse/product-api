using System;
using System.Collections.Generic;
using Moq.EntityFrameworkCore;
using ProductApi.Api.Dtos.Put;
using ProductApi.Api.Exceptions;
using ProductApi.Data.Models;
using Xunit;

namespace ProductApi.UnitTests.Services.ProductOptionsService
{
    public class UpdateTests : ProductOptionsServiceTests
    {
        private readonly Guid _productId;
        private readonly List<ProductOption> _productOptionData;

        private readonly Guid _productOptionId;

        public UpdateTests()
        {
            // arrange
            _productOptionId = Guid.NewGuid();
            _productId = Guid.NewGuid();

            _productOptionData = new List<ProductOption>
            {
                new ProductOption
                {
                    Id = 1, UniqueId = _productOptionId, ProductId = _productId
                }
            };

            MockDbContext.Setup(x => x.ProductOptions).ReturnsDbSet(_productOptionData);
        }

        [Fact]
        private void should_update_the_product_option_in_the_database()
        {
            // arrange
            var productOptionDto = new ProductOptionDto
            {
                Description = "desc",
                Name = "name"
            };

            // act
            ProductOptionsService.Update(_productId, _productOptionId, productOptionDto);

            // assert
            Assert.Single(_productOptionData);
            Assert.Equal(_productOptionId, _productOptionData[0].UniqueId);
            Assert.Equal(_productId, _productOptionData[0].ProductId);
            Assert.Equal(productOptionDto.Description, _productOptionData[0].Description);
            Assert.Equal(productOptionDto.Name, _productOptionData[0].Name);
        }

        [Fact]
        private void should_return_404_if_supplied_product_option_id_does_not_exist()
        {
            // act / assert
            Assert.Throws<NotFoundException>(() =>
                ProductOptionsService.Update(_productId, new Guid("00000000-0000-0000-0000-000000000000"),
                    new ProductOptionDto()));
        }

        [Fact]
        private void should_return_404_if_supplied_product_id_does_not_exist()
        {
            // act / assert
            Assert.Throws<NotFoundException>(() =>
                ProductOptionsService.Update(new Guid("00000000-0000-0000-0000-000000000000"), _productOptionId,
                    new ProductOptionDto()));
        }
    }
}