using SumTL.DAL.Interfaces;
using SumTL.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.DAL.Repos
{
    internal class ImageRepo : Repository<Image>, IImage
    {
        private readonly ApplicationDbContext _db;
        public ImageRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
