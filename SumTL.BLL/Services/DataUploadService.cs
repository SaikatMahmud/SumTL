using ClosedXML.Excel;
using SumTL.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.BLL.Services
{
    public static class DataUploadService
    {
        public static List<Item> ParseItemData(Stream stream)
        {
            var items = new List<Item>();
            using (var workbook = new XLWorkbook(stream))
            {
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RangeUsed().RowsUsed().Skip(1);
                foreach (var row in rows)
                {
                    var item = new Item
                    {
                        ItemName = row.Cell(1).GetValue<string>(),
                        ItemUnit = row.Cell(2).GetValue<string>(),
                        Quantity = row.Cell(3).GetValue<int>(),
                        CategoryId = row.Cell(4).GetValue<int>(),
                    };
                    items.Add(item);
                }
            }
            return items;
        }

        public static List<Category> ParseCategoryData(Stream stream)
        {
            var categories = new List<Category>();
            using (var workbook = new XLWorkbook(stream))
            {
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RangeUsed().RowsUsed().Skip(1);
                foreach (var row in rows)
                {
                    var catagory = new Category
                    {
                        CategoryName = row.Cell(1).GetValue<string>(),
                      
                    };
                    categories.Add(catagory);
                }
            }
            return categories;
        }
    }
}
