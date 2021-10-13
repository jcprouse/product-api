using System;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Api.Dtos.Post;
using ProductApi.Api.Exceptions;
using ProductApi.Api.Filters;
using ProductApi.Api.Services.Interfaces;

namespace ProductApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] ProductsFilter filter)
        {
            return Ok(_productsService.GetAll(filter));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                return Ok(_productsService.Get(id));
            }
            catch (NotFoundException e)
            {
                return new NotFoundObjectResult(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(ProductDto productDto)
        {
            var response = _productsService.Create(productDto);
            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Update(Guid id, Dtos.Put.ProductDto productDto)
        {
            try
            {
                _productsService.Update(id, productDto);
            }
            catch (NotFoundException e)
            {
                return new NotFoundObjectResult(e.Message);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _productsService.Delete(id);
            }
            catch (NotFoundException e)
            {
                return new NotFoundObjectResult(e.Message);
            }

            return new NoContentResult();
        }
    }
}