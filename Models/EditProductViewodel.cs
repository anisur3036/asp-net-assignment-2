using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Models
{
    public class EditProductViewModel : ProductCreateViewModel
    {
        public int Id { get; set; }
    }
}