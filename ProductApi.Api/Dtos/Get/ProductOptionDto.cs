using System;
using ProductApi.Api.Dtos.Base;

namespace ProductApi.Api.Dtos.Get
{
    public class ProductOptionDto : ProductOptionBaseDto
    {
        public Guid Id { get; set; }
    }
}