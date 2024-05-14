using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
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
    public class CategoryService
    {
        private readonly IUnitOfWork DataAccess;
        public CategoryService(IUnitOfWork _dataAccess)
        {
            DataAccess = _dataAccess;
        }
        //public List<CategoryCategoryDTO> Get()
        //{
        //    var data = DataAccess.Category.Get();
        //    if (data != null)
        //    {
        //        var cfg = new MapperConfiguration(c =>
        //        {
        //            c.CreateMap<Category, CategoryCategoryDTO>();
        //            c.CreateMap<Category, CategoryDTO>();
        //        });
        //        var mapper = new Mapper(cfg);
        //        return mapper.Map<List<CategoryCategoryDTO>>(data);

        //    }
        //    return null;
        //}
        public List<CategoryDTO> Get(string? properties = null)
        {
            var data = DataAccess.Category.Get(properties);
            if (data != null)
            {
                var cfg = new MapperConfiguration(c =>
                {
                    c.CreateMap<Category, CategoryDTO>();
                });
                var mapper = new Mapper(cfg);
                return mapper.Map<List<CategoryDTO>>(data);

            }
            return null;
        }
        public CategoryDTO Get(Expression<Func<CategoryDTO, bool>> filter, string? properties = null)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Category, CategoryDTO>();
            });

            var mapper = new Mapper(cfg);
            var categoryFilter = mapper.MapExpression<Expression<Func<Category, bool>>>(filter);

            var data = DataAccess.Category.Get(categoryFilter, properties);
            if (data != null)
            {
                return mapper.Map<CategoryDTO>(data);
            }
            return null;
        }
        public bool Create(CategoryDTO obj)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<CategoryDTO, Category>();
            });
            var mapper = new Mapper(cfg);
            var Category = mapper.Map<Category>(obj);
            return DataAccess.Category.Create(Category);
        }
        public bool Update(CategoryDTO obj)
        {
            var existingData = DataAccess.Category.Get(c => c.Id == obj.Id);
            if (existingData != null)
            {
                existingData.CategoryName = obj.CategoryName;
            }
            return DataAccess.Category.Update(existingData);
        }
        public bool Delete(int Id)
        {
            var data = DataAccess.Category.Get(c => c.Id == Id);
            return DataAccess.Category.Delete(data);
        }
    }


}
