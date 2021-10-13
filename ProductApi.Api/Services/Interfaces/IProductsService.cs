using System;
using ProductApi.Api.Dtos.Get;
using ProductApi.Api.Dtos.Get.Collections;
using ProductApi.Api.Filters;

namespace ProductApi.Api.Services.Interfaces
{
    public interface IProductsService
    {
        ProductCollectionDto GetAll(ProductsFilter filter);

        ProductDto Get(Guid id);

        ProductDto Create(Dtos.Post.ProductDto productDto);

        void Update(Guid id, Dtos.Put.ProductDto productDto);

        void Delete(Guid id);
    }
}