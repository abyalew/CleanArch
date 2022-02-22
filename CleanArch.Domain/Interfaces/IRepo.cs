using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CleanArch.Domain.Interfaces
{
    public interface IRepo<T> where T: class{
        void BeginTransaction();
        void Commit();
        IEnumerable<T> GetAll();
        T FirstOrDefault(Expression<Func<T, bool>> filter); 
        IEnumerable<T> Find(Expression<Func<T, bool>> filter,params Expression<Func<T, object>> [] includes);
        IEnumerable<T> Find(Expression<Func<T, bool>> filter);
        T Add(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
