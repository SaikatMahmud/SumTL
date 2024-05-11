﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.DAL.Interfaces
{
    public interface IRepo<T> where T : class
    {
        IEnumerable<T> Get(string? includeProperties = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
        bool Create(T obj);
        bool Update(T obj);
        bool Delete(T obj);
    }
}
