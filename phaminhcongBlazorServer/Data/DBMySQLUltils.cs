using MySql.Data.MySqlClient;

namespace phaminhcongBlazorServer.Data
{
    public class DBMySQLUltils
    {
        private static string Host = "localhost";
        private static string UserName = "root";
        private static string Password = "";
        private static int Port = 3306;
        private static string Database = "category";

        public static MySqlConnection GetMySqlConnection()
        {
            string connString = String.Format("Server={0};Database={1};User={2};Password={3};Port={4}",Host, Database,UserName,Password,Port);
            return new MySqlConnection(connString);
        }
    }
}
