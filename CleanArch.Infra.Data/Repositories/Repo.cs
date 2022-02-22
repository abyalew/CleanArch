using CleanArch.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CleanArch.Infra.Data.Repositories
{
    public class Repo<T> : IRepo<T> where T : class
    {
        internal readonly DbContext _context;
        internal IDbContextTransaction _transaction;
        public Repo(DbContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                if (_transaction != null)
                {
                    _transaction.Commit();
                }
            }
            catch (Exception)
            {
                _transaction?.Rollback();
                throw;
            }
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
            return Include(_context.Set<T>().Where(filter), includes);
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

        protected static IQueryable<TEntity> Include<TEntity>(IQueryable<TEntity> query,
            params Expression<Func<TEntity, object>>[] includes) where TEntity : class
        {
            IQueryable<TEntity> queryable = null;
            foreach (var include in includes)
            {
                queryable = query.Include(include);
            }
            return queryable;
        }
    }
}
