using System;
using System.Collections.Generic;
using Moq.EntityFrameworkCore;
using ProductApi.Data.Models;
using Xunit;

namespace ProductApi.UnitTests.Services.ProductOptionsService
{
    public class GetAllTests : ProductOptionsServiceTests
    {
        private readonly Guid _productId;
        private readonly Guid _productOptionId1;
        private readonly Guid _productOptionId2;

        public GetAllTests()
        {
            // arrange
            _productId = Guid.NewGuid();
            _productOptionId1 = Guid.NewGuid();
            _productOptionId2 = Guid.NewGuid();

            var productOptionData = new List<ProductOption>
            {
                new ProductOption
                {
                    Id = 1, UniqueId = _productOptionId1, Description = "desc 1", Name = "name 1",
                    ProductId = _productId
                },
                new ProductOption
                {
                    Id = 2, UniqueId = Guid.NewGuid(), Description = "desc 2", Name = "name 2",
                    ProductId = new Guid("00000000-0000-0000-0000-000000000000")
                },
                new ProductOption
                {
                    Id = 3, UniqueId = _productOptionId2, Description = "desc 3", Name = "name 3",
                    ProductId = _productId
                }
            };

            MockDbContext.Setup(x => x.ProductOptions).ReturnsDbSet(productOptionData);
        }

        [Fact]
        private void should_return_all_product_options_for_a_single_product_from_the_database()
        {
            // act
            var response = ProductOptionsService.GetAll(_productId);

            // assert
            Assert.Equal(2, response.Items.Count);
            Assert.Equal(_productOptionId1, response.Items[0].Id);
            Assert.Equal(_productOptionId2, response.Items[1].Id);
        }

        [Fact]
        private void should_return_an_empty_array_if_no_product_options_can_be_found()
        {
            // act
            var response = ProductOptionsService.GetAll(new Guid("99999999-9999-9999-9999-999999999999"));

            // assert
            Assert.Empty(response.Items);
        }
    }
}