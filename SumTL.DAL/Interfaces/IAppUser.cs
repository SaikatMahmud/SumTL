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
        bool Login(T obj, out string? errorMsg);
        bool Logout(T obj, out string? errorMsg);
        bool ChangePass(T obj, out string? errorMsg);
    }
}
