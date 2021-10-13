using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductApi.Api.Controllers;
using ProductApi.Api.Dtos.Get;
using ProductApi.Api.Dtos.Get.Collections;
using ProductApi.Api.Exceptions;
using ProductApi.Api.Services.Interfaces;
using Xunit;

namespace ProductApi.UnitTests.Controllers
{
    public class ProductOptionsControllerTests
    {
        private readonly Mock<IProductOptionsService> _mockProductOptionsService;

        private readonly Guid _productId;
        private readonly ProductOptionsController _productOptionsController;

        public ProductOptionsControllerTests()
        {
            _mockProductOptionsService = new Mock<IProductOptionsService>();
            _productOptionsController = new ProductOptionsController(_mockProductOptionsService.Object);

            _productId = new Guid();
        }

        public class GetAllTests : ProductOptionsControllerTests
        {
            [Fact]
            private void should_return_product_option_collection_returned_from_product_option_service()
            {
                // arrange
                var productOptionCollectionDto = new ProductOptionCollectionDto();
                _mockProductOptionsService.Setup(x => x.GetAll(_productId)).Returns(productOptionCollectionDto);

                // act
                var response = _productOptionsController.GetAll(_productId);

                // assert
                var result = response as OkObjectResult;
                Assert.Equal(productOptionCollectionDto, result.Value);
            }
        }

        public class GetTests : ProductOptionsControllerTests
        {
            [Fact]
            private void should_return_the_product_option_with_the_supplied_id_from_product_option_service()
            {
                // arrange
                var id = Guid.NewGuid();
                var productOptionDto = new ProductOptionDto();
                _mockProductOptionsService.Setup(x => x.Get(_productId, id)).Returns(productOptionDto);

                // act
                var response = _productOptionsController.Get(_productId, id);

                // assert
                var result = response as OkObjectResult;
                Assert.Equal(productOptionDto, result.Value);
            }

            [Fact]
            private void should_return_a_not_found_result_if_service_throws_a_not_found_exception()
            {
                // arrange
                var id = Guid.NewGuid();
                _mockProductOptionsService.Setup(x => x.Get(_productId, id)).Throws(new NotFoundException(""));

                // act
                var response = _productOptionsController.Get(_productId, id);

                // assert
                Assert.Equal(typeof(NotFoundObjectResult), response.GetType());
            }
        }

        public class PostTests : ProductOptionsControllerTests
        {
            [Fact]
            private void should_supply_product_option_service_with_supplied_object_and_return_the_new_id()
            {
                // arrange
                var productOptionDto = new Api.Dtos.Post.ProductOptionDto();

                var newId = Guid.NewGuid();
                var responseProductOptionDto = new ProductOptionDto { Id = newId };
                _mockProductOptionsService.Setup(x => x.Create(_productId, productOptionDto))
                    .Returns(responseProductOptionDto);

                // act
                var response = _productOptionsController.Create(_productId, productOptionDto);

                // assert;
                var result = response as CreatedAtActionResult;
                Assert.Equal(responseProductOptionDto, result.Value);
            }

            [Fact]
            private void should_return_a_not_found_result_if_service_throws_a_not_found_exception()
            {
                // arrange
                var productOptionDto = new Api.Dtos.Post.ProductOptionDto();
                _mockProductOptionsService.Setup(x => x.Create(_productId, productOptionDto)).Throws(new NotFoundException(""));

                // act
                var response = _productOptionsController.Create(_productId, productOptionDto);

                // assert
                Assert.Equal(typeof(NotFoundObjectResult), response.GetType());
            }
        }

        public class PutTests : ProductOptionsControllerTests
        {
            [Fact]
            private void should_supply_product_options_service_with_supplied_object_and_id()
            {
                // arrange
                var productOptionDto = new Api.Dtos.Put.ProductOptionDto();
                var newId = Guid.NewGuid();

                // act
                _productOptionsController.Update(_productId, newId, productOptionDto);

                // assert
                _mockProductOptionsService.Verify(x => x.Update(_productId, newId, productOptionDto));
            }

            [Fact]
            private void should_return_a_not_found_result_if_service_throws_a_not_found_exception()
            {
                // arrange
                var productOptionDto = new Api.Dtos.Put.ProductOptionDto();
                var newId = Guid.NewGuid();
                _mockProductOptionsService.Setup(x => x.Update(_productId, newId, productOptionDto)).Throws(new NotFoundException(""));

                // act
                var response = _productOptionsController.Update(_productId, newId, productOptionDto);

                // assert
                Assert.Equal(typeof(NotFoundObjectResult), response.GetType());
            }
        }

        public class DeleteTests : ProductOptionsControllerTests
        {
            [Fact]
            private void should_supply_product_options_service_with_supplied_id()
            {
                // arrange
                var newId = Guid.NewGuid();

                // act
                _productOptionsController.Delete(_productId, newId);

                // assert
                _mockProductOptionsService.Verify(x => x.Delete(_productId, newId));
            }

            [Fact]
            private void should_return_a_not_found_result_if_service_throws_a_not_found_exception()
            {
                // arrange
                var newId = Guid.NewGuid();
                _mockProductOptionsService.Setup(x => x.Delete(_productId, newId)).Throws(new NotFoundException(""));

                // act
                var response = _productOptionsController.Delete(_productId, newId);

                // assert
                Assert.Equal(typeof(NotFoundObjectResult), response.GetType());
            }
        }
    }
}