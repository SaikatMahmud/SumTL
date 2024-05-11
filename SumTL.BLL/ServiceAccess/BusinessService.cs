using Microsoft.EntityFrameworkCore.ChangeTracking;
using SumTL.BLL.Services;
using SumTL.DAL;
using SumTL.DAL.Models;
using SumTL.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.BLL.ServiceAccess
{
    public class BusinessService : IBusinessService
    {
        // public IUnitOfWork _unitOfWork { get; set; }
        public CategoryService CategoryService { get; set; }
        public ItemService ItemService { get; set; }

        public BusinessService(IUnitOfWork unitOfWork)
        {
            //_unitOfWork = unitOfWork;
            CategoryService = new CategoryService(unitOfWork);
            ItemService = new ItemService(unitOfWork);
        }
    }
}
