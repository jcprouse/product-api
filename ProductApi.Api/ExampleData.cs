using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Data;
using ProductApi.Data.Models;

namespace ProductApi.Api
{
    public class ExampleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Products.Any() || context.ProductOptions.Any()) return;

                context.Products.AddRange(
                    new Product
                    {
                        UniqueId = new Guid("001457bd-b551-4b49-85b8-e91243d5e590"),
                        Name = "Product1",
                        Description = "Description 1",
                        Price = new decimal(1.11),
                        DeliveryPrice = new decimal(2.22)
                    },
                    new Product
                    {
                        UniqueId = new Guid("2347970f-79c8-46dc-a8e8-c2123a068f90"),
                        Name = "Product2",
                        Description = "Description 2",
                        Price = new decimal(3.33),
                        DeliveryPrice = new decimal(4.44)
                    },
                    new Product
                    {
                        UniqueId = new Guid("fa96e7fd-521d-4a4f-930e-79f0e6c71931"),
                        Name = "Product3",
                        Description = "Description 3",
                        Price = new decimal(5.55),
                        DeliveryPrice = new decimal(6.66)
                    },
                    new Product
                    {
                        //Id = 4,
                        UniqueId = new Guid("20bfafce-097e-477d-a388-15dd8b1a4de1"),
                        Name = "Product4",
                        Description = "Description 4",
                        Price = new decimal(7),
                        DeliveryPrice = new decimal(7.5)
                    },
                    new Product
                    {
                        UniqueId = new Guid("91020387-f330-4fe4-8a98-0b49a79b11e7"),
                        Name = "Product5",
                        Description = "Description 5",
                        Price = new decimal(8),
                        DeliveryPrice = new decimal(8.5)
                    },
                    new Product
                    {
                        //Id = 6,
                        UniqueId = new Guid("cae64693-3206-4e49-a4f3-51014a1b2b4a"),
                        Name = "Product6",
                        Description = "Description 6",
                        Price = new decimal(9),
                        DeliveryPrice = new decimal(9.5)
                    }, new Product
                    {
                        UniqueId = new Guid("569b4f81-c330-4504-8709-6e6995103c4c"),
                        Name = "Product7",
                        Description = "Description 7",
                        Price = new decimal(1.11),
                        DeliveryPrice = new decimal(2.22)
                    },
                    new Product
                    {
                        UniqueId = new Guid("b62c38de-ee60-4b19-bae0-b8f45e90209e"),
                        Name = "Product8",
                        Description = "Description 8",
                        Price = new decimal(3.33),
                        DeliveryPrice = new decimal(4.44)
                    },
                    new Product
                    {
                        UniqueId = new Guid("ee61a94d-13ef-4de4-9296-286c1a3cff5e"),
                        Name = "Product9",
                        Description = "Description 9",
                        Price = new decimal(5.55),
                        DeliveryPrice = new decimal(6.66)
                    },
                    new Product
                    {
                        //Id = 10,
                        UniqueId = new Guid("92c652fc-1a42-4f37-9ef1-7b9cddcc6cc3"),
                        Name = "Product10",
                        Description = "Description 10",
                        Price = new decimal(7),
                        DeliveryPrice = new decimal(7.5)
                    },
                    new Product
                    {
                        UniqueId = new Guid("9fc3d2d7-be38-4286-b822-97ef0ccc10d1"),
                        Name = "Product11",
                        Description = "Description 11",
                        Price = new decimal(8),
                        DeliveryPrice = new decimal(8.5)
                    },
                    new Product
                    {
                        UniqueId = new Guid("6c30fb9d-7ad8-42ff-bfc7-64d80ca2bd1f"),
                        Name = "Product12",
                        Description = "Description 12",
                        Price = new decimal(9),
                        DeliveryPrice = new decimal(9.5)
                    });

                context.ProductOptions.AddRange(
                    new ProductOption
                    {
                        Description = "Description 1",
                        Name = "Product Option 1",
                        ProductId = new Guid("001457bd-b551-4b49-85b8-e91243d5e590"), // Product 1,
                        UniqueId = new Guid("ddf867e6-a01a-44e5-9cbc-97bfd9b7eefb")
                    },
                    new ProductOption
                    {
                        Description = "Description 2",
                        Name = "Product Option 2",
                        ProductId = new Guid("001457bd-b551-4b49-85b8-e91243d5e590"), // Product 1,
                        UniqueId = new Guid("ffe079d7-5610-481c-8f61-a22690a62d6d")
                    },
                    new ProductOption
                    {
                        Description = "Description 3",
                        Name = "Product Option 3",
                        ProductId = new Guid("001457bd-b551-4b49-85b8-e91243d5e590"), // Product 1,
                        UniqueId = new Guid("f8ddff48-9f5c-4f59-8a15-05be5d081565")
                    },
                    new ProductOption
                    {
                        Description = "Description 4",
                        Name = "Product Option 4",
                        ProductId = new Guid("2347970f-79c8-46dc-a8e8-c2123a068f90"), // Product 2,
                        UniqueId = new Guid("4dfa6a50-c2bc-44a5-8a0c-03af7c623598")
                    }
                );
                context.SaveChanges();
            }
        }
    }
}