using CleanArch.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CleanArch.Infra.Data.Repositories
{
    public class Repo<T> : IRepo<T> where T : class
    {
        internal readonly DbContext _context;

        public Repo(DbContext context)
        {
            _context = context;
        }

        public T Add(T entity)
        {
            var entry = _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entry.Entity;
        }

        public T Delete(T entity)
        {
            var entry = _context.Set<T>().Remove(entity);
            _context.SaveChanges();
            return entry.Entity;
        }

        public T FirstOrDefault(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().FirstOrDefault(filter);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().Where(filter).AsEnumerable();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> filter,params Expression<Func<T, object>> [] includes)
        {
            return Include(includes,_context.Set<T>().Where(filter));
        }



        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public T Update(T entity)
        {
            var entry = _context.Set<T>().Update(entity);
            _context.SaveChanges();
            return entry.Entity;
        }

        private static IQueryable<TEntity> Include<TEntity>(
            Expression<Func<TEntity, object>>[] includes, 
            IQueryable<TEntity> query) where TEntity : class
        {
            IQueryable<TEntity> queryable = query;
            foreach (var include in includes)
            {
                queryable = queryable.Include(include);
            }
            return queryable;
        }
    }
}
