using AutoMapper;
using SumTL.BLL.DTOs;
using SumTL.DAL.Models;
using SumTL.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.BLL.Services
{
    public class ItemService
    {
        private readonly IUnitOfWork DataAccess;
        public ItemService(IUnitOfWork _dataAccess)
        {
            DataAccess = _dataAccess;
        }
        public List<ItemCategoryDTO> Get()
        {
            var data = DataAccess.Item.Get("Category");
            if (data != null)
            {
                var cfg = new MapperConfiguration(c =>
                {
                    c.CreateMap<Item, ItemCategoryDTO>();
                    c.CreateMap<Category, CategoryDTO>();

                });
                var mapper = new Mapper(cfg);
                return mapper.Map<List<ItemCategoryDTO>>(data);

            }
            return null;
        }
        public ItemCategoryDTO Get(Expression<Func<Item, bool>> filter)
        {
            var data = DataAccess.Item.Get(filter, "Category");
            if (data != null)
            {
                var cfg = new MapperConfiguration(c =>
                {
                    c.CreateMap<Item, ItemCategoryDTO>();
                    c.CreateMap<Category, CategoryDTO>();

                });
                var mapper = new Mapper(cfg);
                return mapper.Map<ItemCategoryDTO>(data);

            }
            return null;
        }
    }
}
