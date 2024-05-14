using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.BLL.DTOs
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemUnit { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
    }
}
