using System;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Api.Dtos.Post;
using ProductApi.Api.Exceptions;
using ProductApi.Api.Services.Interfaces;

namespace ProductApi.Api.Controllers
{
    [ApiController]
    [Route("api/products/{productId:guid}/options")]
    public class ProductOptionsController : ControllerBase
    {
        private readonly IProductOptionsService _productOptionsService;

        public ProductOptionsController(IProductOptionsService productOptionsService)
        {
            _productOptionsService = productOptionsService;
        }

        [HttpGet]
        public IActionResult GetAll(Guid productId)
        {
            return Ok(_productOptionsService.GetAll(productId));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult Get(Guid productId, Guid id)
        {
            try
            {
                return Ok(_productOptionsService.Get(productId, id));
            }
            catch (NotFoundException e)
            {
                return new NotFoundObjectResult(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(Guid productId, ProductOptionDto productOptionDto)
        {
            try
            {
                var response = _productOptionsService.Create(productId, productOptionDto);
                return CreatedAtAction(nameof(Get), new { productId, id = response.Id }, response);
            }
            catch (NotFoundException e)
            {
                return new NotFoundObjectResult(e.Message);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Update(Guid productId, Guid id, Dtos.Put.ProductOptionDto productOptionDto)
        {
            try
            {
                _productOptionsService.Update(productId, id, productOptionDto);
            }
            catch (NotFoundException e)
            {
                return new NotFoundObjectResult(e.Message);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult Delete(Guid productId, Guid id)
        {
            try
            {
                _productOptionsService.Delete(productId, id);
            }
            catch (NotFoundException e)
            {
                return new NotFoundObjectResult(e.Message);
            }
            return new NoContentResult();
        }
    }
}