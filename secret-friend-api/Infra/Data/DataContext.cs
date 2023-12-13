using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Infra.Data
{
    public class DataContext
    {
        public MySqlConnection connection { get; }
        public DataContext(IConfiguration configuration)
        {
            connection = new MySqlConnection(configuration.GetConnectionString("MySqlConnection"));
        }
    }
}
