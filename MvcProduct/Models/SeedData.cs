using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcProduct.Data;
using System;
using System.Linq;
using MvcProduct.Models;
namespace MvcProduct.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new AppDbContext(
           serviceProvider.GetRequiredService<
               DbContextOptions<AppDbContext>>()))
        {
            // Look for any movies.
            if (context.Product.Any())
            {
                return;   // DB has been seeded
            }
            context.Customers.AddRange(
                new Customer
                {
                    FirstName = "Иван",
                    LastName = "Петров",
                    Email = "ivan.petrov@example.com",
                    Phone = "+79161234567",
                    City = "Москва"
                },
                new Customer
                {
                    FirstName = "Елена",
                    LastName = "Сидорова",
                    Email = "elena.sidorova@example.com",
                    Phone = "+79269876543",
                    City = "Санкт-Петербург"
                }
               );
            context.Product.AddRange(
                new Product
                {
                    Name = "Холодильник Samsung RB33J",
                    Description = "Двухкамерный холодильник с No Frost",
                    Price = 54990M,
                    Category = "Холодильники",
                    StockQuantity = 15,
                    ProductionDate = new DateTime(2023, 1, 15),
                    EnergyClass = "A++",
                    Rating = 5,
                    WarrantyMonths = 36
                },
                new Product
                {
                    Name = "Телевизор LG 55NANO",
                    Description = "Телевизор 55\" 4K UHD Smart TV",
                    Price = 69.990M,
                    Category = "Телевизоры",
                    StockQuantity = 8,
                    ProductionDate = new DateTime(2023, 3, 10),
                    EnergyClass = "A+",
                    Rating = 2,
                    WarrantyMonths = 24
                },
                new Product
                {
                    Name = "Стиральная машина Bosch WAT",
                    Description = "Стиральная машина с загрузкой 7 кг",
                    Price = 42990M,
                    Category = "Стиральные машины",
                    StockQuantity = 12,
                    ProductionDate = new DateTime(2022, 11, 5),
                    EnergyClass = "A+++",
                    Rating = 4,
                    WarrantyMonths = 60
                },
                new Product
                {
                    Name = "Микроволновая печь Panasonic",
                    Description = "Микроволновка с грилем 25 литров",
                    Price = 12990M,
                    Category = "Мелкая техника",
                    StockQuantity = 20,
                    ProductionDate = new DateTime(2023, 2, 20),
                    EnergyClass = "B",
                    Rating = 4,
                    WarrantyMonths = 12
                }
            );
            context.SaveChanges();
            if (context.Orders.Any())
            {
                return;   // DB has been seeded
            }
            var customers = context.Customers.ToList();
            var products = context.Product.ToList();

            context.Orders.AddRange(
                new Order
                {
                    CustomerId = customers[0].Id,
                    OrderDate = DateTime.Now.AddDays(-3),
                    Status = "В обработке",
                    Products = new List<Product> { products[0], products[1] }
                },
                new Order
                    {
                CustomerId = customers[1].Id,
                OrderDate = DateTime.Now.AddDays(-1),
                Status = "Доставлен",
                Products = new List<Product> { products[2] }
                }
            );
            context.SaveChanges();
        }
    }
}