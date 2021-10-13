using System;
using System.Collections.Generic;
using Moq.EntityFrameworkCore;
using ProductApi.Api.Filters;
using ProductApi.Data.Models;
using Xunit;

namespace ProductApi.UnitTests.Services.ProductsService
{
    public class GetAllTests : ProductsServiceTests
    {
        private readonly ProductsFilter _filter;
        private readonly List<Product> _productData;

        public GetAllTests()
        {
            // arrange
            _productData = new List<Product>
            {
                new Product
                {
                    Id = 1, UniqueId = Guid.NewGuid(), Description = "desc 1", DeliveryPrice = new decimal(1),
                    Name = "name 1",
                    Price = new decimal(1.1)
                },
                new Product
                {
                    Id = 2, UniqueId = Guid.NewGuid(), Description = "desc 2", DeliveryPrice = new decimal(2),
                    Name = "name 2",
                    Price = new decimal(2.1)
                },
                new Product
                {
                    Id = 3, UniqueId = Guid.NewGuid(), Description = "desc 3", DeliveryPrice = new decimal(3),
                    Name = "name 3",
                    Price = new decimal(3.1)
                }
            };

            MockDbContext.Setup(x => x.Products).ReturnsDbSet(_productData);

            _filter = new ProductsFilter { Limit = 10, Offset = 0 };
        }

        [Fact]
        private void should_return_all_products_from_the_database()
        {
            // act
            var response = ProductsService.GetAll(_filter);

            // assert
            Assert.Equal(3, response.Items.Count);
            Assert.Equal(_productData[0].UniqueId, response.Items[0].Id);
            Assert.Equal(_productData[1].UniqueId, response.Items[1].Id);
            Assert.Equal(_productData[2].UniqueId, response.Items[2].Id);
        }

        [Fact]
        private void should_return_products_ordered_by_id_ascending()
        {
            // arrange
            var firstProductUniqueId = _productData[0].UniqueId;
            var secondProductUniqueId = _productData[1].UniqueId;
            var thirdProductUniqueId = _productData[2].UniqueId;

            var product = _productData[0];
            _productData.Remove(product);
            _productData.Add(product);

            // act
            var response = ProductsService.GetAll(_filter);

            // assert
            Assert.Equal(3, response.Items.Count);
            Assert.Equal(firstProductUniqueId, response.Items[0].Id);
            Assert.Equal(secondProductUniqueId, response.Items[1].Id);
            Assert.Equal(thirdProductUniqueId, response.Items[2].Id);
        }

        [Fact]
        private void should_return_a_filtered_list_of_products()
        {
            // arrange
            _filter.Limit = 1;
            _filter.Offset = 1;

            // act
            var response = ProductsService.GetAll(_filter);

            // assert
            Assert.Single(response.Items);
            Assert.Equal(_productData[1].UniqueId, response.Items[0].Id);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 10)]
        private void should_return_an_empty_array_if_no_products_can_be_found_in_the_filter_criteria(int limit,
            int offset)
        {
            // arrange
            _filter.Limit = limit;
            _filter.Offset = offset;

            // act
            var response = ProductsService.GetAll(_filter);

            // assert
            Assert.Empty(response.Items);
        }

        [Theory]
        [InlineData("DAD", true)]
        [InlineData("DADDY", true)]
        [InlineData("GRANDAD", true)]
        [InlineData("GRANDADDY", true)]
        [InlineData("dad", true)]
        [InlineData("Dad", true)]
        [InlineData("D AD", false)]
        [InlineData("DA", false)]
        [InlineData("ADD", false)]
        private void should_filter_on_the_name_property_if_provided(string existingProductName, bool shouldBeReturned)
        {
            // arrange
            _filter.Name = "DAD";
            _productData.Add(new Product { Name = existingProductName });

            // act
            var response = ProductsService.GetAll(_filter);

            // assert
            Assert.Equal(shouldBeReturned ? 1 : 0, response.Items.Count);
        }
    }
}