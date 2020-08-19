using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.Json;

namespace HC.CarsDealer.Persistence.LocalDb
{
    public class JsonSourceManager<T> : IJsonSourceManager<T> {
        private readonly string _sourceUri;

        public JsonSourceManager(string sourceUri) {
            _sourceUri = sourceUri;
        }
        public List<T> GetSourceContent()
        {
            var content = GetFileContent(_sourceUri);
            var context = DeserializeContent(content);

            return context;
        }

        private string GetFileContent(string uri) {
            string sourceContent = string.Empty;

            if (!File.Exists(_sourceUri))
            {
                File.WriteAllText(_sourceUri, "[]");
            }

            using (var reader = new StreamReader(_sourceUri))
            {
                sourceContent = reader.ReadToEnd();
            }

            return sourceContent;
        }

        private List<T> DeserializeContent(string content) {
            var context = JsonSerializer.Deserialize<List<T>>(content);

            return context;
        }

        public bool SaveContext(List<T> context)
        {
            try
            {
                var content = JsonSerializer.Serialize(context);
                File.WriteAllText(_sourceUri, content);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
