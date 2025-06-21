using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Inventory.Data;
using Inventory.Models.Entities;
using Inventory.Repositories.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Repositories.Implementations
{
    public class CategoryRepositories : ICategoryRepositories
    {
        private readonly InventoryDbContext _context;
        public CategoryRepositories(InventoryDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetCategoriesByOrderAsync(string orderBy = "Name")
        {
            return await _context.Categories.OrderBy(c => c.Name).ToListAsync();
        }

        public SelectList GetSelectedList(int? id = null)
        {
            return new SelectList(
                _context.Categories.OrderBy(c => c.Name),
                "Id",
                "Name"
            );
        }
    }
}