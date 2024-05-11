using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.DAL.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemUnit { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

    }
}
