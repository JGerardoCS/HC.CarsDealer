using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace HC.CarsDealer.Persistence.LocalDb
{
    public interface IJsonSourceManager<T>
    {
        List<T> GetSourceContent();
        bool SaveContext(List<T> context);
    }
}
