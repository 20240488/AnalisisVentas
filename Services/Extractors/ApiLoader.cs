using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnalisisVentas.Interfaces;
using System.Text.Json;

namespace AnalisisVentas.Services
{
    public class ApiLoader<T> : IExternalSource<T>
    {
        private readonly string _url;

        public ApiLoader(string url)
        {
            _url = url;
        }

        public List<T> Extract()
        {
            using var client = new HttpClient();
            var response = client.GetStringAsync(_url).Result;
            return JsonSerializer.Deserialize<List<T>>(response);
        }
    }
}