﻿using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.AspNetCore.Mvc;
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
        //public List<ItemCategoryDTO> Get()
        //{
        //    var data = DataAccess.Item.Get("Category");
        //    if (data != null)
        //    {
        //        var cfg = new MapperConfiguration(c =>
        //        {
        //            c.CreateMap<Item, ItemCategoryDTO>();
        //            c.CreateMap<Category, CategoryDTO>();

        //        });
        //        var mapper = new Mapper(cfg);
        //        return mapper.Map<List<ItemCategoryDTO>>(data);

        //    }
        //    return null;
        //}
        //public ItemCategoryDTO Get(Expression<Func<Item, bool>> filter)
        //{
        //    var data = DataAccess.Item.Get(filter, "Category");
        //    if (data != null)
        //    {
        //        var cfg = new MapperConfiguration(c =>
        //        {
        //            c.CreateMap<Item, ItemCategoryDTO>();
        //            c.CreateMap<Category, CategoryDTO>();

        //        });
        //        var mapper = new Mapper(cfg);
        //        return mapper.Map<ItemCategoryDTO>(data);

        //    }
        //    return null;
        //}


        public List<ItemDTO> Get(string? properties = null)
        {
            var data = DataAccess.Item.Get(properties);
            if (data != null)
            {
                var cfg = new MapperConfiguration(c =>
                {
                    c.CreateMap<Item, ItemDTO>();
                    c.CreateMap<Category, CategoryDTO>();
                });
                var mapper = new Mapper(cfg);
                return mapper.Map<List<ItemDTO>>(data);

            }
            return null;
        }
        public ItemImagesDTO Get(Expression<Func<ItemDTO, bool>> filter, string? properties = null)
        {
            var cfg = new MapperConfiguration(c =>
            {
                //c.AddExpressionMapping();
                c.CreateMap<Item, ItemDTO>(); // only for expression
                c.CreateMap<Item, ItemImagesDTO>();
                c.CreateMap<Image, ImageDTO>();
            });
            var mapper = new Mapper(cfg);
            var itemFilter = mapper.MapExpression<Expression<Func<Item, bool>>>(filter);


            var data = DataAccess.Item.Get(itemFilter, properties);
            if (data != null)
            {
                return mapper.Map<ItemImagesDTO>(data);
            }
            return null;
        }

        public List<ItemDTO> GetCustomized(int skip, int take, out int totalCount, out int filteredCount, string? properties = null)
        {
            totalCount = 0;
            filteredCount = 0;
            var data = DataAccess.Item.GetCustomizedListData(skip, take, properties);
            if (data.Item1 != null)
            {
                var cfg = new MapperConfiguration(c =>
                {
                    c.CreateMap<Item, ItemDTO>();
                    c.CreateMap<Category, CategoryDTO>();
                });
                var mapper = new Mapper(cfg);
                totalCount = data.Item2;
                filteredCount = data.Item3;
                return mapper.Map<List<ItemDTO>>(data.Item1);

            }
            return null;
        }

        public List<ItemDTO> GetCustomized(Expression<Func<ItemDTO, bool>> filter, int skip, int take, out int totalCount, out int filteredCount, string? properties = null)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Item, ItemDTO>();
                c.CreateMap<Category, CategoryDTO>();
            });
            var mapper = new Mapper(cfg);
            var itemFilter = mapper.MapExpression<Expression<Func<Item, bool>>>(filter);
            totalCount = 0;
            filteredCount = 0;
            var data = DataAccess.Item.GetCustomizedListData(itemFilter, skip, take, properties);
            if (data.Item1 != null)
            {
                totalCount = data.Item2;
                filteredCount = data.Item3;
                return mapper.Map<List<ItemDTO>>(data.Item1);

            }
            return null;
        }

        public bool Create(ItemDTO obj)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<ItemDTO, Item>();
            });
            var mapper = new Mapper(cfg);
            var Item = mapper.Map<Item>(obj);
            return DataAccess.Item.Create(Item);
        }
        public bool Update(ItemDTO obj)
        {
            var existingData = DataAccess.Item.Get(c => c.Id == obj.Id);
            if (existingData != null)
            {
                existingData.ItemName = obj.ItemName;
                existingData.ItemUnit = obj.ItemUnit;
                existingData.Quantity = obj.Quantity;
                existingData.CategoryId = obj.CategoryId;
            }
            return DataAccess.Item.Update(existingData);
        }
        public bool Delete(int Id)
        {
            var data = DataAccess.Item.Get(c => c.Id == Id);
            return DataAccess.Item.Delete(data);
        }

        public bool UploadImage(int Id, string ImageUrl)
        {
            var obj = new Image()
            {
                ItemId = Id,
                ImageUrl = ImageUrl
            };
            return DataAccess.Image.Create(obj);
        }
        public string DeleteImage(int ImageId)
        {
            var data = DataAccess.Image.Get(c => c.Id == ImageId);
            DataAccess.Image.Delete(data);
            return data.ImageUrl;
        }
        public async Task<bool> UploadFromExcel(Stream stream)
        {
            var data = DataUploadService.ParseItemData(stream);
            if (data != null)
            {
               var result = await DataAccess.Item.UploadBulk(data);
                if(result.success) return true;
            }
            return false;
        }
    }
}
