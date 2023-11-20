using ClassLibrary1;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Data.UserManagement;
using SchoolApp.DataAccess.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DataAccess.Implementations
{
    public class DataAccess<T> : IDataAccess<T> where T : Person
    {

        private readonly SchoolAppContext _context;
        public DataAccess(SchoolAppContext context)
        {
            _context = context;
        }
        public async Task<T> Add(T entity)
        {
            var addedItem = await _context.AddAsync<T>(entity);
            await SaveChangesAsync();
            return addedItem.Entity;
        }

        public IQueryable<T> GetAll()
        {
            var items = _context.Set<T>().AsQueryable();
            return items;
        }

        public async Task<T> GetById(int id)
        {
            var item = await _context.Set<T>().FindAsync(id);
            return item;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
