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
        Task<T> Add(T entity);
        Task<T> GetById(int id);
        Task<bool> SaveChangesAsync();
    }
}
