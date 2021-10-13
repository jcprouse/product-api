using System;
using System.Linq;
using ProductApi.Api.Converters;
using ProductApi.Api.Dtos.Get;
using ProductApi.Api.Dtos.Get.Collections;
using ProductApi.Api.Exceptions;
using ProductApi.Api.Services.Interfaces;
using ProductApi.Data;
using ProductApi.Data.Models;

namespace ProductApi.Api.Services
{
    public class ProductOptionsService : IProductOptionsService
    {
        private readonly IApplicationDbContext _dbContext;

        public ProductOptionsService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProductOptionCollectionDto GetAll(Guid productId)
        {
            var productOptions = _dbContext.ProductOptions.Where(x => x.ProductId == productId).OrderBy(x => x.Id)
                .ToList();

            return new ProductOptionCollectionDto
                { Items = productOptions.Select(ProductOptionConverter.ConvertToDto).ToList() };
        }

        public ProductOptionDto Get(Guid productId, Guid id)
        {
            var productOption = GetProductOptionByUniqueIds(id, productId);
            return ProductOptionConverter.ConvertToDto(productOption);
        }

        public ProductOptionDto Create(Guid productId, Dtos.Post.ProductOptionDto productOptionDto)
        {
            if (_dbContext.Products.All(x => x.UniqueId != productId)) throw new NotFoundException("Product not found");

            var productOption = ProductOptionConverter.ConvertToModel(productOptionDto, productId);
            productOption.UniqueId = Guid.NewGuid();
            _dbContext.ProductOptions.Add(productOption);
            _dbContext.SaveChanges();
            return ProductOptionConverter.ConvertToDto(productOption);
        }

        public void Update(Guid productId, Guid id, Dtos.Put.ProductOptionDto productOptionDto)
        {
            var productOption = GetProductOptionByUniqueIds(id, productId);
            productOption.Description = productOptionDto.Description;
            productOption.Name = productOptionDto.Name;
            _dbContext.SaveChanges();
        }

        public void Delete(Guid productId, Guid id)
        {
            var productOption = GetProductOptionByUniqueIds(id, productId);
            _dbContext.ProductOptions.Remove(productOption);
            _dbContext.SaveChanges();
        }

        private ProductOption GetProductOptionByUniqueIds(Guid id, Guid productId)
        {
            var productOption =
                _dbContext.ProductOptions.FirstOrDefault(x => x.UniqueId == id && x.ProductId == productId);

            if (productOption == null) throw new NotFoundException("Product option");

            return productOption;
        }
    }
}