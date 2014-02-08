using System;
using System.Collections.Generic;

namespace Petuda.Model.DDD.DALContracts
{
    public interface IPetudaDAO<T> where T : new()
    {
        T GetByID(Guid id);
        ICollection<T> GetAll();
        void Save(T item);
        void Remove(Guid id);
    }
}