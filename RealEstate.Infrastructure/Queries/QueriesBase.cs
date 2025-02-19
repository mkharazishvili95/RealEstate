using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace RealEstate.Infrastructure.Queries
{
    public abstract class QueriesBase
    {
        private readonly string _connectionString;

        public QueriesBase(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<TResponse?> Get<TResponse>(string commandText, Func<SqlDataReader, TResponse> selector) where TResponse : new()
        {
            TResponse? response = default;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(commandText, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                response = selector(reader);
                            }
                        }
                    }
                }
            }

            return response;
        }

        public async Task<List<TResponse>> GetMany<TResponse>(string commandText, Func<SqlDataReader, TResponse> selector)
        {
            var responses = new List<TResponse>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(commandText, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var value = selector(reader);
                            responses.Add(value);
                        }
                    }
                }
            }

            return responses;
        }
    }
}
