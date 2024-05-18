using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SumTL.BLL.DTOs;
using SumTL.DAL.Models;
using SumTL.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.BLL.Services
{
    public class AuthService
    {
        private readonly IUnitOfWork DataAccess;
        public AuthService(IUnitOfWork _dataAccess)
        {
            DataAccess = _dataAccess;
        }
        public bool Register(AppUserDTO obj, out string? errorMsg)
        {
            errorMsg = null;
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<AppUserDTO, AppUser>();
            });
            var mapper = new Mapper(cfg);
            var AppUser = mapper.Map<AppUser>(obj);
            var result = DataAccess.AppUser.Register(AppUser);
            if (!result.Result.success)
            {
                errorMsg = result.Result.error;
                return false;
            }
            return true;
        }
        public bool Login(AppUserDTO obj, out string? errorMsg)
        {
            errorMsg = null;
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<AppUserDTO, AppUser>();
            });
            var mapper = new Mapper(cfg);
            var AppUser = mapper.Map<AppUser>(obj);
            var result = DataAccess.AppUser.Login(AppUser);
            if (!result.Result.success)
            {
                errorMsg = result.Result.error;
                return false;
            }
            return true;
        }
        public bool Logout()
        {
            DataAccess.AppUser.Logout();
            return true;
        }
    }
}
