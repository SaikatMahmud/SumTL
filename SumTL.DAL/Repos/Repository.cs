﻿using Microsoft.EntityFrameworkCore;
using SumTL.DAL.Interfaces;
using SumTL.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.DAL.Repos
{
    internal class Repository<T> : IRepo<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public bool Create(T obj)
        {
            dbSet.Add(obj);
            if (_db.SaveChanges() > 0) return true;
            return false;
        }

        public bool Delete(T obj)
        {
           dbSet.Remove(obj);
            return _db.SaveChanges() > 0;
        }

        public IEnumerable<T> Get(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        public bool Update(T obj)
        {
            dbSet.Update(obj);
            return _db.SaveChanges() > 0;
        }

        public (IEnumerable<T>, int TotalCount, int FilteredCount) GetCustomizedListData(Expression<Func<T, bool>> filter, int skip, int take, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            int totalCount = dbSet.Count();
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            int filterCount = query.Count();  
            query = query.Skip(skip).Take(take);
            return (query.ToList(), totalCount, filterCount);
        } 
        public (IEnumerable<T>, int TotalCount, int FilteredCount) GetCustomizedListData(int skip, int take, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            int totalCount = dbSet.Count();
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            int filteredCount = query.Count();
            query = query.Skip(skip).Take(take);
            return (query.ToList(), totalCount, filteredCount);
        }

    }
}
