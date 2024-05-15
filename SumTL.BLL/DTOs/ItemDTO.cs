using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.BLL.DTOs
{
    public class ItemDTO
    {
        public int Id { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public string ItemUnit { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public CategoryDTO Category { get; set; }
    }
}
