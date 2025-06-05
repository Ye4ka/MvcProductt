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
                    FirstName = "����",
                    LastName = "������",
                    Email = "ivan.petrov@example.com",
                    Phone = "+79161234567",
                    City = "������"
                },
                new Customer
                {
                    FirstName = "�����",
                    LastName = "��������",
                    Email = "elena.sidorova@example.com",
                    Phone = "+79269876543",
                    City = "�����-���������"
                }
               );
            context.Product.AddRange(
                new Product
                {
                    Name = "����������� Samsung RB33J",
                    Description = "������������ ����������� � No Frost",
                    Price = 54990M,
                    Category = "������������",
                    StockQuantity = 15,
                    ProductionDate = new DateTime(2023, 1, 15),
                    EnergyClass = "A++",
                    Rating = 5,
                    WarrantyMonths = 36
                },
                new Product
                {
                    Name = "��������� LG 55NANO",
                    Description = "��������� 55\" 4K UHD Smart TV",
                    Price = 69.990M,
                    Category = "����������",
                    StockQuantity = 8,
                    ProductionDate = new DateTime(2023, 3, 10),
                    EnergyClass = "A+",
                    Rating = 2,
                    WarrantyMonths = 24
                },
                new Product
                {
                    Name = "���������� ������ Bosch WAT",
                    Description = "���������� ������ � ��������� 7 ��",
                    Price = 42990M,
                    Category = "���������� ������",
                    StockQuantity = 12,
                    ProductionDate = new DateTime(2022, 11, 5),
                    EnergyClass = "A+++",
                    Rating = 4,
                    WarrantyMonths = 60
                },
                new Product
                {
                    Name = "������������� ���� Panasonic",
                    Description = "������������� � ������ 25 ������",
                    Price = 12990M,
                    Category = "������ �������",
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
                    Status = "� ���������",
                    Products = new List<Product> { products[0], products[1] }
                },
                new Order
                    {
                CustomerId = customers[1].Id,
                OrderDate = DateTime.Now.AddDays(-1),
                Status = "���������",
                Products = new List<Product> { products[2] }
                }
            );
            context.SaveChanges();
        }
    }
}