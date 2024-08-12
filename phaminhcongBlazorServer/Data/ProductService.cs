using MySql.Data.MySqlClient;

namespace phaminhcongBlazorServer.Data
{
    public class ProductService
    {
        public async Task<int> GetLowStockProductCountAsync()
        {
            int lowStockCount = 0;

            using (var connection = DBMySQLUltils.GetMySqlConnection())
            {
                await connection.OpenAsync();
                string query = "SELECT COUNT(*) FROM products WHERE prd_quantity < 10";
                using (var command = new MySqlCommand(query, connection))
                {
                    lowStockCount = Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }

            return lowStockCount;
        }
    }
}
