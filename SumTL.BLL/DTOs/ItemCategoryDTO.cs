using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.BLL.DTOs
{
    public class ItemCategoryDTO : ItemDTO
    {
        public CategoryDTO Category { get; set; }
    }
}
