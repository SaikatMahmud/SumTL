using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SumTL.DAL.Interfaces;
using SumTL.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
            //Add items to the database directly using EF
            //await _db.Items.AddRangeAsync(items);
            //await _db.SaveChangesAsync();          
            //return (true, null);

            try
            {
                string json = JsonConvert.SerializeObject(items);
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
                dt.Columns.Remove("Id");
                dt.Columns.Remove("Category");
                dt.Columns.Remove("Images");

                //var rowCount = new SqlParameter
                //{
                //    ParameterName = "@insertCount",
                //    SqlDbType = SqlDbType.Int,
                //    Direction = ParameterDirection.Output,
                //};

                var list = new SqlParameter("@ItemList", SqlDbType.Structured)
                {
                    TypeName = "ItemType",
                    Value = dt
                };

               // var result = _db.Database.ExecuteSqlRaw("spInsertItems @ItemList", table);
                var result = _db.Database.ExecuteSqlRaw("spInsertItems {0}", list);
                //int overallCount = (int)rowCount.Value;
                if(result != items.Count)
                {
                    return (true, "Some items have dublicate names and excluded from the list.");
                }
                return (true, "All items insterted");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
