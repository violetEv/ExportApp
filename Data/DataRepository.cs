using ExportApp.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System;

namespace ExportApp.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly string? _connectionString;

        public DataRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("DefaultConnection string not found.");
        }

        public async Task<List<DataModel>> GetDataAsync()
        {
            var list = new List<DataModel>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("sp_GetDataModel", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var item = new DataModel
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Nama = reader.GetString(reader.GetOrdinal("Nama")),
                            Tanggal = reader.GetDateTime(reader.GetOrdinal("Tanggal"))
                        };
                        list.Add(item);
                    }
                }
            }
            return list;
        }
    }
}
