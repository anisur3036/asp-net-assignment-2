using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inventory.Services.Interfaces
{
    public interface ICategoryServices
    {
        Task<List<Category>> GetCategoriesByOrderAsync(string orderBy = "Name");

        SelectList GetSelectedList(int? id = null);
    }
}