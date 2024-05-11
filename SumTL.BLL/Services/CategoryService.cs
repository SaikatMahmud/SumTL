using AutoMapper;
using SumTL.BLL.DTOs;
using SumTL.DAL.Models;
using SumTL.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.BLL.Services
{
    public class CategoryService
    {
        private readonly IUnitOfWork DataAccess;
        public CategoryService(IUnitOfWork _dataAccess)
        {
            DataAccess = _dataAccess;
        }
        public List<CategoryItemDTO> Get()
        {
            var data = DataAccess.Category.Get();
            if (data != null)
            {
                var cfg = new MapperConfiguration(c =>
                {
                    c.CreateMap<Category, CategoryItemDTO>();
                    c.CreateMap<Item, ItemDTO>();
                });
                var mapper = new Mapper(cfg);
                return mapper.Map<List<CategoryItemDTO>>(data);

            }
            return null;
        }
    }


}
