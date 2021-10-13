using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Moq.EntityFrameworkCore;
using ProductApi.Api.Exceptions;
using ProductApi.Data.Models;
using Xunit;

namespace ProductApi.UnitTests.Services.ProductOptionsService
{
    public class DeleteTests : ProductOptionsServiceTests
    {
        private readonly Guid _productId;
        private readonly List<ProductOption> _productOptionData;
        private readonly Guid _productOptionIdToKeep;
        private readonly Guid _productOptionIdToRemove;

        public DeleteTests()
        {
            _productOptionIdToRemove = Guid.NewGuid();
            _productOptionIdToKeep = Guid.NewGuid();
            _productId = Guid.NewGuid();

            _productOptionData = new List<ProductOption>
            {
                new ProductOption
                {
                    Id = 1, UniqueId = _productOptionIdToRemove, ProductId = _productId
                },
                new ProductOption
                {
                    Id = 2, UniqueId = _productOptionIdToKeep, ProductId = _productId
                }
            };
            MockDbContext.Setup(x => x.ProductOptions).ReturnsDbSet(_productOptionData);

            MockDbContext.Setup(x => x.ProductOptions.Remove(It.IsAny<ProductOption>()))
                .Callback((ProductOption p) => _productOptionData.Remove(p));
        }

        [Fact]
        private void should_remove_the_product_option_from_the_database()
        {
            // act
            ProductOptionsService.Delete(_productId, _productOptionIdToRemove);

            // assert
            Assert.Single(_productOptionData);
            Assert.Equal(_productOptionIdToKeep, _productOptionData.First().UniqueId);
        }

        [Fact]
        private void should_return_404_if_supplied_product_option_id_does_not_exist()
        {
            // act / assert
            Assert.Throws<NotFoundException>(() =>
                ProductOptionsService.Delete(_productId, new Guid("00000000-0000-0000-0000-000000000000")));
        }

        [Fact]
        private void should_return_404_if_supplied_product_id_does_not_exist()
        {
            // act / assert
            Assert.Throws<NotFoundException>(() =>
                ProductOptionsService.Delete(new Guid("00000000-0000-0000-0000-000000000000"),
                    _productOptionIdToRemove));
        }
    }
}