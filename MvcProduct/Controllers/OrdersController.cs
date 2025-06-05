using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcProduct.Data;
using MvcProduct.Models;

namespace MvcProduct.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(string orderStatus, string customerSearch, string productSearch)
        {
            if (_context.Orders == null)
            {
                return Problem("Сущность 'AppDbContext.Orders' равна null.");
            }

   
            IQueryable<string> statusQuery = from o in _context.Orders
                                             orderby o.Status
                                             select o.Status;

         
            var orders = from o in _context.Orders
                         .Include(o => o.Customer)
                         .Include(o => o.Products)
                         select o;

   
            if (!string.IsNullOrEmpty(customerSearch))
            {
                orders = orders.Where(o =>
                    o.Customer.FirstName.Contains(customerSearch) ||
                    o.Customer.LastName.Contains(customerSearch));
            }

    
            if (!string.IsNullOrEmpty(orderStatus))
            {
                orders = orders.Where(o => o.Status == orderStatus);
            }

            if (!string.IsNullOrEmpty(productSearch))
            {
                orders = orders.Where(o =>
                    o.Products.Any(p => p.Name.Contains(productSearch)));
            }

            var viewModel = new OrderViewModel
            {
                Statuses = new SelectList(await statusQuery.Distinct().ToListAsync()),
                Orders = await orders.ToListAsync(),
                OrderStatus = orderStatus,
                CustomerSearch = customerSearch,
                ProductSearch = productSearch
            };

            return View(viewModel);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Products)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FirstName");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,OrderDate,Status")] Order order, List<int> selectedProducts)
        {
            if (ModelState.IsValid)
            {
              
                if (selectedProducts != null)
                {
                    foreach (var productId in selectedProducts)
                    {
                        var product = await _context.Products.FindAsync(productId);
                        if (product != null)
                        {
                            order.Products.Add(product);
                        }
                    }
                }

                order.Status = order.Status ?? "Новый";
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FirstName", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Products)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FirstName", order.CustomerId);
            ViewData["SelectedProducts"] = new MultiSelectList(_context.Products, "Id", "Name", order.Products.Select(p => p.Id));

            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,OrderDate,Status")] Order order, List<int> selectedProducts)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
           
                    var existingOrder = await _context.Orders
                        .Include(o => o.Products)
                        .FirstOrDefaultAsync(o => o.Id == id);

           
                    existingOrder.CustomerId = order.CustomerId;
                    existingOrder.OrderDate = order.OrderDate;
                    existingOrder.Status = order.Status;
                    existingOrder.Products.Clear();
                    if (selectedProducts != null)
                    {
                        foreach (var productId in selectedProducts)
                        {
                            var product = await _context.Products.FindAsync(productId);
                            if (product != null)
                            {
                                existingOrder.Products.Add(product);
                            }
                        }
                    }

                    _context.Update(existingOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FirstName", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Products)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}