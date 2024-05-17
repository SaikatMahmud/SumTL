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
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<AppUserDTO, AppUser>();
            });
            var mapper = new Mapper(cfg);
            var AppUser = mapper.Map<AppUser>(obj);
            return DataAccess.AppUser.Register(AppUser, out errorMsg);
        }
    }
}
