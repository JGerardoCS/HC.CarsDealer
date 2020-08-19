using HC.CarsDealer.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.CarsDealer.Application.Abstractions
{
    public interface IProductBusiness
    {
        Response<ProductModel> Create(ProductModel product);
        Response<IEnumerable<ProductModel>> GetAll();
        Response<ProductModel> Get(int id);
        Response Update(ProductModel product);
        Response Delete(int id);
    }
}
