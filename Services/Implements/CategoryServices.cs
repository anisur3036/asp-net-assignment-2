using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Models.Entities;
using Inventory.Repositories.interfaces;
using Inventory.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inventory.Services.Implements
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepositories _categoryRepositories;
        public CategoryServices(ICategoryRepositories categoryRepositories)
        {
            _categoryRepositories = categoryRepositories;
        }

        public async Task<List<Category>> GetCategoriesByOrderAsync(string orderBy = "Name")
        {
            return await _categoryRepositories.GetCategoriesByOrderAsync(orderBy);
        }

        public SelectList GetSelectedList(int? id)
        {
            return _categoryRepositories.GetSelectedList(id);
        }
    }
}