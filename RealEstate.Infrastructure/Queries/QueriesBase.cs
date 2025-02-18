using Microsoft.Extensions.Configuration;
using Npgsql;

namespace RealEstate.Infrastructure.Queries
{
    public abstract class QueriesBase
    {
        string connectionString { get; set; }
        public QueriesBase(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<TResponse?> Get<TResponse>(string commandText, Func<NpgsqlDataReader, TResponse> selector) where TResponse : new()
        {
            var response = new TResponse();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    using var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            response = await reader.Select(selector);
                        }
                        return response;
                    }
                }
                await connection.CloseAsync();
            }
            return default;
        }

        public async Task<List<TResponse>> GetMany<TResponse>(string commandText, Func<NpgsqlDataReader, TResponse> selector)
        {
            var response = new List<TResponse>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    using var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                        while (await reader.ReadAsync())
                        {
                            var value = await reader.Select(selector);
                            response.Add(value);
                        }
                }
                await connection.CloseAsync();
            }
            return response;
        }
    }

    public static class QuerieHelpler
    {
        public static async Task<T> Select<T>(this NpgsqlDataReader reader, Func<NpgsqlDataReader, T> selector)
        {
            return selector.Invoke(reader);
        }
    }
}
