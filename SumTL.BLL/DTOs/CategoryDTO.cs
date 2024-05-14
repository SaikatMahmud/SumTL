using SumTL.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.BLL.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }

    }
}
