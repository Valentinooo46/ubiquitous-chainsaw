using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OLX.Context;

namespace OLX.Implements
{
    public class Repository<T>(OLXContext context) : Interfaces.IRepository<T> where T : class
    {
        private readonly OLXContext _context = context;
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return [.. _dbSet];
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IQueryable<T> GetQuery()
        {
            return _dbSet.AsQueryable();
        }
    }
}
