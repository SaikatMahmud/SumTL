using Microsoft.AspNetCore.Identity;
using SumTL.DAL.Interfaces;
using SumTL.DAL.Models;
using SumTL.DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public ICategory Category { get; private set; }
        public IItem Item { get; private set; }
        public IAppUser<AppUser> AppUser { get; set; }
        public UnitOfWork(ApplicationDbContext db, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;

            Category = new CategoryRepo(_db);
            Item = new ItemRepo(_db);
            AppUser = new AppUserRepo(_db,_userManager,_signInManager);
        }

    }
}
