using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.BLL.DTOs
{
    public class CategoryItemDTO : CategoryDTO
    {
        public List<ItemDTO> Items { get; set; }
    }
}
