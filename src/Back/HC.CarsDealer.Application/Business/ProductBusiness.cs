using HC.CarsDealer.Application.Abstractions;
using HC.CarsDealer.Application.Models;
using HC.CarsDealer.Domain.Entities;
using HC.CarsDealer.Domain.Interfaces.PersistenceSupport;
using HC.CarsDealer.Persistence.LocalDb;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace HC.CarsDealer.Application.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private IRepository<Product> _dbContext;
        
        public ProductBusiness(IRepository<Product> context) {
            _dbContext = context;
        }

        public Response<ProductModel> Create(ProductModel product) {
            try {
                var prd = new Product {
                    Id = 0,
                    Brand = product.Brand,
                    Description = product.Description,
                    Kilometers = product.Kilometers,
                    Model = product.Model,
                    Price = product.Price,
                    Year = product.Year
                };
                int id = _dbContext.Create(prd);
                product.Id = id;

                return new Response<ProductModel> {
                    Data = product,
                    IsSuccess = true,
                    Message = string.Empty
                };
            }
            catch (Exception) {
                return new Response<ProductModel> {
                    Data = null,
                    IsSuccess = false,
                    Message = string.Empty
                };
            }
        }

        public Response<IEnumerable<ProductModel>> GetAll()
        {
            var data = _dbContext.GetAll().Select(p => new ProductModel {
                Id = p.Id,
                Brand = p.Brand,
                Kilometers = p.Kilometers,
                Description = p.Description,
                Model = p.Model,
                Price = p.Price,
                Year = p.Year
            }).ToList();
            return new Response<IEnumerable<ProductModel>>
            {
                Data = data,
                IsSuccess = true,
                Message = string.Empty
            };
        }

        public Response<ProductModel> Get(int id)
        {
            var p = _dbContext.Get(id);
            var product = new ProductModel
            {
                Id = p.Id,
                Brand = p.Brand,
                Kilometers = p.Kilometers,
                Description = p.Description,
                Model = p.Model,
                Price = p.Price,
                Year = p.Year
            };

            return new Response<ProductModel>
            {
                Data = product,
                IsSuccess = true,
                Message = string.Empty
            };
        }

        public Response Update(ProductModel product)
        {
            var prd = new Product
            {
                Id = product.Id,
                Brand = product.Brand,
                Description = product.Description,
                Kilometers = product.Kilometers,
                Model = product.Model,
                Price = product.Price,
                Year = product.Year
            };
            var result = _dbContext.Update(prd);
            return new Response
            {
                IsSuccess = true,
                Message = string.Empty
            };
        }

        public Response Delete(int id)
        {
            var result = _dbContext.Delete(id);
            return new Response
            {
                IsSuccess = result,
                Message = string.Empty
            };
        }
    }
}
