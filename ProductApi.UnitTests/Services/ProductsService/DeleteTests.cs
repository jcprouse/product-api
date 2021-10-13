using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Moq.EntityFrameworkCore;
using ProductApi.Api.Exceptions;
using ProductApi.Data.Models;
using Xunit;

namespace ProductApi.UnitTests.Services.ProductsService
{
    public class DeleteTests : ProductsServiceTests
    {
        private readonly List<Product> _productData;
        private readonly Guid _productIdToKeep;
        private readonly Guid _productIdToRemove;

        public DeleteTests()
        {
            _productIdToRemove = Guid.NewGuid();
            _productIdToKeep = Guid.NewGuid();

            _productData = new List<Product>
            {
                new Product
                {
                    Id = 1, UniqueId = _productIdToRemove
                },
                new Product
                {
                    Id = 2, UniqueId = _productIdToKeep
                }
            };
            MockDbContext.Setup(x => x.Products).ReturnsDbSet(_productData);

            MockDbContext.Setup(x => x.Products.Remove(It.IsAny<Product>()))
                .Callback((Product p) => _productData.Remove(p));
        }

        [Fact]
        private void should_remove_the_product_from_the_database()
        {
            // act
            ProductsService.Delete(_productIdToRemove);

            // assert
            Assert.Single(_productData);
            Assert.Equal(_productIdToKeep, _productData.First().UniqueId);
        }

        [Fact]
        private void should_return_404_if_supplied_product_id_does_not_exist()
        {
            // act / assert
            Assert.Throws<NotFoundException>(() =>
                ProductsService.Delete(new Guid("00000000-0000-0000-0000-000000000000")));
        }
    }
}