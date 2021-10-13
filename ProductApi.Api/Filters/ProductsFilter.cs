namespace ProductApi.Api.Filters
{
    public class ProductsFilter
    {
        public ProductsFilter()
        {
            Limit = 10;
            Offset = 0;
        }

        public int Limit { get; set; }
        public int Offset { get; set; }
        public string Name { get; set; }
    }
}