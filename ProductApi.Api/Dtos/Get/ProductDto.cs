using System;
using ProductApi.Api.Dtos.Base;

namespace ProductApi.Api.Dtos.Get
{
    public class ProductDto : ProductBaseDto
    {
        public Guid Id { get; set; }
    }
}