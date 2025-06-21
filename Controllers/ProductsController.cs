using Microsoft.AspNetCore.Mvc;
using Inventory.Data;
using Inventory.Models.Entities;
using Inventory.Services.Interfaces;
using System.Threading.Tasks;

namespace Inventory.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductServices _productServices;
        private readonly ICategoryServices _categoryServices;

        public ProductsController(
            IProductServices productServices,
            ICategoryServices categoryServices
        )
        {
            _productServices = productServices;
            _categoryServices = categoryServices;
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchName, string categoryFilter)
        {
            ViewBag.SearchName = searchName;
            ViewBag.CategoryFilter = categoryFilter;
            ViewBag.Categories = await _categoryServices.GetCategoriesByOrderAsync("Name");

            return View(await _productServices.GetFilterdProductsAsync(searchName, categoryFilter));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
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
            ViewData["CategoryId"] = _categoryServices.GetSelectedList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CategoryId,Price,Quantity")] Product product)
        {
            ViewData["CategoryId"] = _categoryServices.GetSelectedList(product.CategoryId);

            if (!ModelState.IsValid)
                return View(product);

            await _productServices.AddProduct(product);
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var product = await _context.Products.FindAsync(id);
            var product = await _productServices.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = _categoryServices.GetSelectedList(product.CategoryId);
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

            ViewData["CategoryId"] = _categoryServices.GetSelectedList(product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var product = await _productServices.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            // await _productServices.DeleteProductAsync(product);

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
                await _productServices.DeleteProductAsync(product);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
