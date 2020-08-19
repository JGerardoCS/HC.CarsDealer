using HC.CarsDealer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;

namespace HC.CarsDealer.Persistence.LocalDb
{
    public class LocalDbContext<T> : IDbContext<T> where T : BaseEntity
    {
        private readonly IJsonSourceManager<T> _sourceManager;
        private List<T> _data;
        
        public LocalDbContext(IJsonSourceManager<T> sourceManager) {
            _sourceManager = sourceManager;
            _data = _sourceManager.GetSourceContent();
        }

        public int Create(T obj) {
            try {
                var id = 0;
                
                if (_data.Count == 0) {
                    id = 1;
                } else {
                    id = _data[_data.Count - 1].Id + 1;
                }
                 
                obj.Id = id;
                _data.Add(obj);
                
                return id;
            }
            catch (Exception) {
                throw;
            }
        }

        public IEnumerable<T> GetAll() {
            return _data;
        }

        public T Get(int id) {
            return _data.SingleOrDefault(x => x.Id == id);
        }

        public bool Update(T obj) {
            try {
                int index = GetIndexOf(obj);
                _data[index] = obj;
                
                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        private int GetIndexOf(T obj) {
            var tempObj = Get(obj.Id);
            int index = _data.IndexOf(tempObj);

            return index;
        }

        public bool Delete(int id) {
            try {
                var obj = Get(id);

                if (obj == null) {
                    throw new Exception("Object to delete does not exists");
                }

                _data.Remove(obj);

                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        public bool SaveChanges() {
            return _sourceManager.SaveContext(_data);
        }
    }
}
