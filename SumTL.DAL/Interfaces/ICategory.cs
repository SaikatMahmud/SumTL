﻿using SumTL.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.DAL.Interfaces
{
    public interface ICategory : IRepo<Category>
    {
        //any extra functionality here if needed
        Task<(bool success, string? error)> UploadBulk(List<Category> categories);
    }
}
