using System;
using ProductApi.Api.Dtos.Get;
using ProductApi.Api.Dtos.Get.Collections;

namespace ProductApi.Api.Services.Interfaces
{
    public interface IProductOptionsService
    {
        ProductOptionCollectionDto GetAll(Guid productId);

        ProductOptionDto Get(Guid productId, Guid id);

        ProductOptionDto Create(Guid productId, Dtos.Post.ProductOptionDto productOptionDto);

        void Update(Guid productId, Guid id, Dtos.Put.ProductOptionDto productOptionDto);

        void Delete(Guid productId, Guid id);
    }
}