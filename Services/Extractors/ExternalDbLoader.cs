using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnalisisVentas.Interfaces;
using Microsoft.Data.SqlClient;

namespace AnalisisVentas.Services
{
    public class ExternalDbLoader<T> : IExternalSource<T>
    {
        private readonly string _connectionString;
        private readonly string _query;
        private readonly Func<SqlDataReader, T> _map;

        public ExternalDbLoader(string connectionString, string query, Func<SqlDataReader, T> map)
        {
            _connectionString = connectionString;
            _query = query;
            _map = map;
        }

        public List<T> Extract()
        {
            var list = new List<T>();

            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var cmd = new SqlCommand(_query, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(_map(reader));
            }

            return list;
        }
    }
}