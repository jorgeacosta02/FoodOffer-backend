using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Infrastructure
{
    public class MySqlConnectionProvider : IDbConecction
    {
        private readonly string connectionString = "server=localhost;port=3306;database=food_offer;user=root;password=; Persist Security Info=False; Connect Timeout=300";

        public MySqlConnectionProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
