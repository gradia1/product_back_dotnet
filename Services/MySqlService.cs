using MySqlConnector;
using System.Threading.Tasks;

using MyApiProject.Data;
using MyApiProject.Models;

public class MySqlService
{
    private readonly string _connectionString;

    public MySqlService(string connectionString)
    {
        _connectionString = connectionString;
    }
    public async Task UpdateStockAsync(string stockId,int amount){

  var query = "UPDATE Stock SET Amount = Amount - @Amount WHERE id = @stockId";
        
        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@stockId", stockId);
                command.Parameters.AddWithValue("@Amount", amount);
                
                await command.ExecuteNonQueryAsync();
            }
        }

    }


    public async Task<List<StockDto>> GetStocksAsync()
    {
        var query = "SELECT Id, Amount FROM stock";
        var stocks = new List<StockDto>();

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new MySqlCommand(query, connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var stock = new StockDto
                        {
                            Amount = reader.GetInt32("Amount"),
                            Id = reader.GetString("Id")
                        };
                        stocks.Add(stock);
                    }
                }
            }
        }

        return stocks;
    }
}
