using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.BLL.DTOs
{
    public class ItemImagesDTO : ItemDTO
    {
        public List<ImageDTO> Images { get; set; }
    }
}
