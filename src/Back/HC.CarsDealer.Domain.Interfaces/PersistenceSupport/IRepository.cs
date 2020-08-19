using System;
using System.Collections.Generic;
using System.Text;

namespace HC.CarsDealer.Domain.Interfaces.PersistenceSupport
{
    public interface IRepository<T> {
        int Create(T obj);
        IEnumerable<T> GetAll();
        T Get(int id);
        bool Update(T obj);
        bool Delete(int id);
    }
}
