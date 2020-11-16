using System;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        public readonly string ConnectionString;
        public DbSet<T> Entity;

        public BaseRepository(DbSet<T> entity, string connectionString)
        {
            ConnectionString = connectionString;
            Entity = entity;
        }

        public void Add(T entity) => Entity.Add(entity);

        public int Count(Func<T, bool> criteria) => Entity.Count(criteria);

        public void Delete(T entity) => Entity.Remove(entity);

        public bool Exists(Func<T, bool> criteria) => Entity.Any(criteria); 

        public T FirstOrDefault(Func<T, bool> criteria) => Entity.FirstOrDefault(criteria);

        public IEnumerable<T> Get<TReturn>(Func<T, bool> filterCriteria, int? skip = 0, int? take = 10, Func<T, TReturn> sortCriteria = null) =>
            sortCriteria == null ?
                Entity
                    .Where(filterCriteria)
                    .Skip(skip.Value)
                    .Take(take.Value)
                    .ToList()
            :
                Entity
                    .Where(filterCriteria)
                    .Skip(skip.Value)
                    .Take(take.Value)
                    .OrderByDescending(sortCriteria)
                    .ToList();

        public void Update(T entity) => Entity.Update(entity);

    }
}