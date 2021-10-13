using System;
using System.Collections.Generic;
using Moq.EntityFrameworkCore;
using ProductApi.Api.Exceptions;
using ProductApi.Data.Models;
using Xunit;

namespace ProductApi.UnitTests.Services.ProductOptionsService
{
    public class GetTests : ProductOptionsServiceTests
    {
        private readonly Guid _productId;
        private readonly List<ProductOption> _productOptionData;

        private readonly Guid _productOptionId;

        public GetTests()
        {
            // arrange
            _productOptionId = Guid.NewGuid();
            _productId = Guid.NewGuid();

            _productOptionData = new List<ProductOption>
            {
                new ProductOption
                {
                    Id = 1, UniqueId = _productOptionId, ProductId = _productId, Description = "Desc", Name = "Name"
                },
                new ProductOption
                {
                    Id = 2, UniqueId = new Guid("00000000-0000-0000-0000-000000000000"), ProductId = _productId
                }
            };

            MockDbContext.Setup(x => x.ProductOptions).ReturnsDbSet(_productOptionData);

            var productData = new List<Product>
            {
                new Product { Id = 1, UniqueId = _productId }
            };

            MockDbContext.Setup(x => x.Products).ReturnsDbSet(productData);
        }

        [Fact]
        private void should_return_the_product_option_matching_the_option_id_and_product_id_from_the_database()
        {
            // act
            var response = ProductOptionsService.Get(_productId, _productOptionId);

            // assert
            Assert.Equal(_productOptionId, response.Id);
            Assert.Equal(_productOptionData[0].Description, response.Description);
            Assert.Equal(_productOptionData[0].Name, response.Name);
        }

        [Fact]
        private void should_return_404_if_supplied_product_option_id_does_not_exist()
        {
            // act / assert
            Assert.Throws<NotFoundException>(() =>
                ProductOptionsService.Get(_productId, new Guid("99999999-9999-9999-9999-999999999999")));
        }

        [Fact]
        private void should_return_404_if_supplied_product_id_does_not_exist()
        {
            // act / assert
            Assert.Throws<NotFoundException>(() =>
                ProductOptionsService.Get(new Guid("00000000-0000-0000-0000-000000000000"), _productOptionId));
        }
    }
}