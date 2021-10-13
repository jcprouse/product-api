using System;
using System.Linq;
using ProductApi.Api.Converters;
using ProductApi.Api.Dtos.Get;
using ProductApi.Api.Dtos.Get.Collections;
using ProductApi.Api.Exceptions;
using ProductApi.Api.Filters;
using ProductApi.Api.Services.Interfaces;
using ProductApi.Data;
using ProductApi.Data.Models;

namespace ProductApi.Api.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IApplicationDbContext _dbContext;

        public ProductsService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProductCollectionDto GetAll(ProductsFilter filter)
        {
            var productsDbSet = _dbContext.Products.AsEnumerable();

            if (!string.IsNullOrEmpty(filter.Name))
                productsDbSet = productsDbSet.Where(x => IsCaseInsensitiveEqual(x.Name, filter.Name));

            var products = productsDbSet.OrderBy(x => x.Id).Skip(filter.Offset).Take(filter.Limit).ToList();

            return new ProductCollectionDto { Items = products.Select(ProductConverter.ConvertToDto).ToList() };
        }

        public ProductDto Get(Guid id)
        {
            var product = GetProductByUniqueId(id);
            return ProductConverter.ConvertToDto(product);
        }

        public ProductDto Create(Dtos.Post.ProductDto productDto)
        {
            var product = ProductConverter.ConvertToModel(productDto);
            product.UniqueId = Guid.NewGuid();
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return ProductConverter.ConvertToDto(product);
        }

        public void Update(Guid id, Dtos.Put.ProductDto productDto)
        {
            var product = GetProductByUniqueId(id);
            product.DeliveryPrice = productDto.DeliveryPrice;
            product.Description = productDto.Description;
            product.Name = productDto.Name;
            product.Price = productDto.Price;
            _dbContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var product = GetProductByUniqueId(id);
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
        }

        private Product GetProductByUniqueId(Guid uniqueId)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.UniqueId == uniqueId);

            if (product == null) throw new NotFoundException("Product");

            return product;
        }

        private bool IsCaseInsensitiveEqual(string a, string b)
        {
            return a.IndexOf(b, 0, StringComparison.CurrentCultureIgnoreCase) != -1;
        }
    }
}