using SumTL.BLL.Services;
using SumTL.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.BLL.ServiceAccess
{
    public interface IBusinessService
    {
        // public IUnitOfWork _unitOfWork { get; set; }
        public CategoryService CategoryService { get; set; }
        public ItemService ItemService { get; set; }
        public AuthService AuthService { get; set; }
    }
}
