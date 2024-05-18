using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.DAL.Models
{
    public class Image
    {
        public int Id { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }
    }
}
