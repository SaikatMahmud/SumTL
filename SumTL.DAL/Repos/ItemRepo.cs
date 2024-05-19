using Microsoft.EntityFrameworkCore;
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
    internal class ItemRepo : Repository<Item>, IItem
    {
        private readonly ApplicationDbContext _db;
        public ItemRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<(bool success, string? error)> UploadBulk(List<Item> items)
        {
            await _db.Items.AddRangeAsync(items);
            await _db.SaveChangesAsync();          
            return (true, null);
        }
    }
}
