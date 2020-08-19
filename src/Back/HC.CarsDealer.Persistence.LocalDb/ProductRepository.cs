using HC.CarsDealer.Domain.Entities;
using HC.CarsDealer.Domain.Interfaces.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace HC.CarsDealer.Persistence.LocalDb
{
    public class ProductRepository : IRepository<Product> {
        private readonly IDbContext<Product> _context;

        public ProductRepository(IDbContext<Product> context) {
            _context = context;
            _context.SaveChanges();
        }

        public int Create(Product obj) {
            var result = _context.Create(obj);
            _context.SaveChanges();

            return result;
        }

        public Product Get(int id) {
            if (id == 0)
            {
                return new Product();
            }

            var product = _context.Get(id);
            return product;
        }

        public IEnumerable<Product> GetAll() {
            return _context.GetAll();
        }

        public bool Update(Product obj) {
            var result = _context.Update(obj);
            _context.SaveChanges();

            return result;
        }

        public bool Delete(int id)
        {
            var result = _context.Delete(id);
            _context.SaveChanges();

            return result;
        }
    }
}
