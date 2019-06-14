
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NükEM.Repository
{
    public interface IRepository<T> where T:class
    {
        List<T> Listing();
        //decimal GetConstantValue();
        bool Adding(T entity);
        bool Updating(T entity);
        bool Deleting(T entity);
    }
}
