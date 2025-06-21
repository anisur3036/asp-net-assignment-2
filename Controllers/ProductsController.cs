using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inventory.Data;
using Inventory.Models.Entities;
using Inventory.Services.Interfaces;

namespace Inventory.Controllers
{
    public class ProductsController : Controller
    {
        private readonly InventoryDbContext _context;
        private readonly IProductServices _productServices;

        public ProductsController(InventoryDbContext context, IProductServices productServices)
        {
            _context = context;
            _productServices = productServices;
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchName, string categoryFilter)
        {
            ViewBag.SearchName = searchName;
            ViewBag.CategoryFilter = categoryFilter;
            ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();

            return View(await _productServices.GetFilterdProductsAsync(searchName, categoryFilter));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productServices.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CategoryId,Price,Quantity")] Product product)
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);

            if (!ModelState.IsValid)
                return View(product);

            await _productServices.AddProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CategoryId,Price,Quantity")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _productServices.UpdateProductAsync(product);

                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productServices.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _productServices.DeleteProduct(product);

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productServices.GetProductByIdAsync(id);
            if (product != null)
            {
                _productServices.DeleteProduct(product);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
