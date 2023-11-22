using SchoolApp.Data.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DataAccess.Abstractions
{
    public interface IDataAccess<T> where T : Person
    {
        IQueryable<T> GetAll();
        List<T> GetWhere(Func<T, bool> predicate);
        bool Any(Func<T, bool> predicate);
        Task<T> Add(T entity);
        Task<T> GetById(int id);
        void Delete(T entity);
        Task<bool> SaveChangesAsync();
    }
}
