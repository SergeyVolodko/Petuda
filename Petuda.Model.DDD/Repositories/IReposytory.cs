using System;
using System.Collections.Generic;

namespace Petuda.Model.DDD.Repositories
{
    public interface IReposytory<T>
    {
        void Save(T item);
        ICollection<T> LoadAll();
        T Load(Guid id);
        void Remove(Guid id);
    }
}