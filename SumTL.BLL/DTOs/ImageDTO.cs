using SumTL.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.BLL.DTOs
{
    public class ImageDTO
    {
        public int Id { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public Item Item { get; set; }

    }
}
