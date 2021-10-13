using Microsoft.EntityFrameworkCore;
using ProductApi.Data.Models;

namespace ProductApi.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; set; }

        DbSet<ProductOption> ProductOptions { get; set; }

        int SaveChanges();
    }
}