using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductApi.Api.Controllers;
using ProductApi.Api.Dtos.Get;
using ProductApi.Api.Dtos.Get.Collections;
using ProductApi.Api.Exceptions;
using ProductApi.Api.Filters;
using ProductApi.Api.Services.Interfaces;
using Xunit;

namespace ProductApi.UnitTests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductsService> _mockProductsService;
        private readonly ProductsController _productsController;

        public ProductsControllerTests()
        {
            _mockProductsService = new Mock<IProductsService>();
            _productsController = new ProductsController(_mockProductsService.Object);
        }

        public class GetAllTests : ProductsControllerTests
        {
            [Fact]
            private void should_return_product_collection_returned_from_product_service()
            {
                // arrange
                var filter = new ProductsFilter();

                var productCollectionDto = new ProductCollectionDto();
                _mockProductsService.Setup(x => x.GetAll(filter)).Returns(productCollectionDto);

                // act
                var response = _productsController.GetAll(filter);

                // assert
                var result = response as OkObjectResult;
                Assert.Equal(productCollectionDto, result.Value);
            }
        }

        public class GetTests : ProductsControllerTests
        {
            [Fact]
            private void should_return_the_product_with_the_supplied_id_from_product_service()
            {
                // arrange
                var id = Guid.NewGuid();
                var productDto = new ProductDto();
                _mockProductsService.Setup(x => x.Get(id)).Returns(productDto);

                // act
                var response = _productsController.Get(id);

                // assert
                var result = response as OkObjectResult;
                Assert.Equal(productDto, result.Value);
            }

            [Fact]
            private void should_return_a_not_found_result_if_service_throws_a_not_found_exception()
            {
                // arrange
                var id = Guid.NewGuid();
                _mockProductsService.Setup(x => x.Get(id)).Throws(new NotFoundException(""));

                // act
                var response = _productsController.Get(id);

                // assert
                Assert.Equal(typeof(NotFoundObjectResult), response.GetType());
            }
        }

        public class PostTests : ProductsControllerTests
        {
            [Fact]
            private void should_supply_product_service_with_supplied_object_and_returns_the_new_id()
            {
                // arrange
                var requestProductDto = new Api.Dtos.Post.ProductDto();

                var newId = Guid.NewGuid();
                var responseProductDto = new ProductDto { Id = newId };
                _mockProductsService.Setup(x => x.Create(requestProductDto)).Returns(responseProductDto);

                // act
                var response = _productsController.Create(requestProductDto);

                // assert;
                var result = response as CreatedAtActionResult;
                Assert.Equal(responseProductDto, result.Value);
            }
        }

        public class PutTests : ProductsControllerTests
        {
            [Fact]
            private void should_supply_product_service_with_supplied_object_and_id()
            {
                // arrange
                var productDto = new Api.Dtos.Put.ProductDto();
                var newId = Guid.NewGuid();

                // act
                _productsController.Update(newId, productDto);

                // assert
                _mockProductsService.Verify(x => x.Update(newId, productDto));
            }

            [Fact]
            private void should_return_a_not_found_result_if_service_throws_a_not_found_exception()
            {
                // arrange
                var productDto = new Api.Dtos.Put.ProductDto();
                var id = Guid.NewGuid();
                _mockProductsService.Setup(x => x.Update(id, productDto)).Throws(new NotFoundException(""));

                // act
                var response = _productsController.Update(id, productDto);

                // assert
                Assert.Equal(typeof(NotFoundObjectResult), response.GetType());
            }
        }

        public class DeleteTests : ProductsControllerTests
        {
            [Fact]
            private void should_supply_product_service_with_supplied_id()
            {
                // arrange
                var newId = Guid.NewGuid();

                // act
                _productsController.Delete(newId);

                // assert
                _mockProductsService.Verify(x => x.Delete(newId));
            }

            [Fact]
            private void should_return_a_not_found_result_if_service_throws_a_not_found_exception()
            {
                // arrange
                var id = Guid.NewGuid();
                _mockProductsService.Setup(x => x.Delete(id)).Throws(new NotFoundException(""));

                // act
                var response = _productsController.Delete(id);

                // assert
                var result = response as NotFoundObjectResult;

                Assert.Equal(typeof(NotFoundObjectResult), response.GetType());
            }
        }
    }
}