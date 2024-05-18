using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SumTL.DAL.Interfaces
{
    public interface IAppUser<T>
    {
        Task<(bool success, string? error)> Register(T obj);
        Task<(bool success, string? error)> Login(T obj);
        Task<bool> Logout();
        bool ChangePass(T obj, out string? errorMsg);
    }
}
