using Moq;
using ProductApi.Data;

namespace ProductApi.UnitTests.Services.ProductOptionsService
{
    public class ProductOptionsServiceTests
    {
        protected readonly Mock<IApplicationDbContext> MockDbContext;
        protected readonly Api.Services.ProductOptionsService ProductOptionsService;

        public ProductOptionsServiceTests()
        {
            MockDbContext = new Mock<IApplicationDbContext>();
            ProductOptionsService = new Api.Services.ProductOptionsService(MockDbContext.Object);
        }
    }
}