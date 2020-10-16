using System;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        T FirstOrDefault(Func<T, bool> criteria);
        IEnumerable<T> Get<TReturn>(Func<T, bool> filterCriteria, int? skip = 0, int? take = 15, Func<T, TReturn> sortCriteria = null);
        void Update(T entity);
        void Delete(T entity);
        bool Exists(Func<T, bool> criteria);
        int Count(Func<T, bool> criteria);
    }
}