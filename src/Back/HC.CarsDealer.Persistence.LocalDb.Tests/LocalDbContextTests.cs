using HC.CarsDealer.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace HC.CarsDealer.Persistence.LocalDb.Tests
{
    public class LocalDbContextTests
    {
        [Fact]
        public void Create_EmptyDb_ReturnsLess1() {
            var mock = GetMockDependency<Product>();
            var obj = new LocalDbContext<Product>(mock);

            var actual = obj.Create(new Product { });

            Assert.Equal(expected: 1, actual);
        }

        [Fact]
        public void GetAll_EmptyDb_ReturnsEmptyCollection() {
            var mock = GetMockDependency<Product>();
            var obj = new LocalDbContext<Product>(mock);

            var actual = obj.GetAll();

            Assert.Empty(actual);
        }

        [Fact]
        public void Get_Id1OnEmptyDb_ReturnsNull() {
            var mock = GetMockDependency<Product>();
            var obj = new LocalDbContext<Product>(mock);

            var actual = obj.Get(1);

            Assert.Null(actual);
        }

        [Fact]
        public void Update_OnEmptyDb_ReturnsFalse() {
            var mock = GetMockDependency<Product>();
            var obj = new LocalDbContext<Product>(mock);

            var actual = obj.Update(new Product { Id = 1 });

            Assert.False(actual);
        }

        [Fact]
        public void Delete_Id1EmptyDb_ReturnsFalse() {
            var mock = GetMockDependency<Product>();
            var obj = new LocalDbContext<Product>(mock);

            var actual = obj.Delete(1);

            Assert.False(actual);
        }

        private IJsonSourceManager<T> GetMockDependency<T>() where T : BaseEntity {
            var mock = new Mock<IJsonSourceManager<T>>();
            mock.Setup(x => x.GetSourceContent())
                .Returns(() => new List<T>());
            mock.Setup(x => x.SaveContext(It.IsAny<List<T>>()))
                .Returns<List<T>>(a => true);

            return mock.Object;
        }
    }
}
