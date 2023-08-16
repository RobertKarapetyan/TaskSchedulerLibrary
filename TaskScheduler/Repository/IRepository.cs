using System;
using System.Collections.Generic;

namespace TaskSchedulerLibrary.Repository
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(Guid id);
        void Add(T entity);
        void Update(T entity);
        void Delete(Guid id);
    }
}
