using Moq;
using ProductApi.Data;

namespace ProductApi.UnitTests.Services.ProductsService
{
    public class ProductsServiceTests
    {
        protected readonly Mock<IApplicationDbContext> MockDbContext;
        protected readonly Api.Services.ProductsService ProductsService;

        public ProductsServiceTests()
        {
            MockDbContext = new Mock<IApplicationDbContext>();
            ProductsService = new Api.Services.ProductsService(MockDbContext.Object);
        }
    }
}