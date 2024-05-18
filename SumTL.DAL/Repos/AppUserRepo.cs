using Microsoft.AspNetCore.Identity;
using SumTL.DAL.Interfaces;
using SumTL.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.DAL.Repos
{
    internal class AppUserRepo : IAppUser<AppUser>
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        public AppUserRepo(ApplicationDbContext _db, UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager)
        {
            db = _db;
            userManager = _userManager;
            signInManager = _signInManager;
        }
        public async Task<(bool success, string? error)> Register(AppUser obj)
        {
            try
            {
                // var checkUserName = await
                var createResult = await userManager.CreateAsync(obj, obj.PasswordHash);
                if (createResult.Succeeded)
                {
                    var roleResult = await userManager.AddToRoleAsync(obj, "General");
                    if (roleResult.Succeeded) return (true, null);
                    else
                    {
                        _ = userManager.DeleteAsync(obj);
                        return (false, roleResult.Errors.FirstOrDefault().Description.ToString());
                    }
                }
                else return (false, createResult.Errors.FirstOrDefault().Description.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(bool success, string? error)> Login(AppUser obj)
        {
            var result = await signInManager.PasswordSignInAsync(obj.UserName, obj.PasswordHash, false, false);
            if (result.Succeeded) { return (true, null); };
            return (false, "Invalid username or password");
        }

        public async Task<bool> Logout()
        {
            await signInManager.SignOutAsync();
            return true;
        }
        public bool ChangePass(AppUser obj, out string? errorMsg)
        {
            throw new NotImplementedException();
        }



        //public bool Register(AppUser obj, out string? errorMsg)
        //{
        //    errorMsg = "";
        //    try
        //    {
        //        var createResult =  userManager.CreateAsync(obj, obj.Password);
        //        if (createResult.IsCompletedSuccessfully)
        //        {
        //            var roleResult = userManager.AddToRoleAsync(obj, "Admin");
        //            if (roleResult.IsCompletedSuccessfully) return true;
        //            else
        //            {
        //                errorMsg = roleResult.Result.ToString();
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            errorMsg = createResult.Result.ToString();
        //            return false;
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
