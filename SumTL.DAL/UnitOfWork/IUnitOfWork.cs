using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SumTL.DAL.Interfaces;

namespace SumTL.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICategory Category { get; }
        IItem Item { get; }
    }
}
