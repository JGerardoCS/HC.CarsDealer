using HC.CarsDealer.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace HC.CarsDealer.Persistence.LocalDb.Tests
{
    public class ProductRepositoryTests
    {
        [Fact]
        public void GetAll_EmptyLocalDb_ReturnsEmptyCollection()
        {
            var mock = GetMockContext();
            var obj = new ProductRepository(mock);

            var actual = obj.GetAll();
            
            Assert.Empty(actual);
        }

        [Fact]
        public void Create_EmptyLocalDb_Returns1()
        {
            var mock = GetMockContext();
            var obj = new ProductRepository(mock);
            var actual = obj.Create(new Product { });

            Assert.Equal(1, actual);
        }

        private IDbContext<Product> GetMockContext() {
            var mock = new Mock<IDbContext<Product>>();
            mock.Setup(x => x.GetAll())
                .Returns(() => new List<Product>());
            mock.Setup(x => x.Create(It.IsAny<Product>()))
                .Returns<Product>(a => 1);

            return mock.Object;
        }
    }
}
