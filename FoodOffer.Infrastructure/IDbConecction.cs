using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Infrastructure
{
    public interface IDbConecction
    {
        MySqlConnection GetConnection();
    }
}
