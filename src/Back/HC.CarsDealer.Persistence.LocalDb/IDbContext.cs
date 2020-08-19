using System;
using System.Collections.Generic;
using System.Text;

namespace HC.CarsDealer.Persistence.LocalDb
{
    public interface IDbContext<T> {
        int Create(T obj);
        IEnumerable<T> GetAll();
        T Get(int id);
        bool Update(T obj);
        bool Delete(int id);
        bool SaveChanges();
    }
}
