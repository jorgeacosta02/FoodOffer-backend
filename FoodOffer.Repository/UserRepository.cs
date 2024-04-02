using FoodOffer.Infrastructure;
using FoodOffer.Model.Models;
using FoodOffer.Model.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace FoodOffer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbConecction session;
        
        public UserRepository(IDbConecction dbConecction, ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            session = dbConecction ?? throw new ArgumentNullException(nameof(dbConecction));
        }


        public Client GetUser(short userId)      
        {
            Client user = null;

            var query = new StringBuilder();
            query.AppendLine("SELECT * FROM client ");
            query.AppendLine("JOIN adress ON Client_Id = Id ");
            query.AppendLine("WHERE Id = ? ");


            using (var connection = session.GetConnection())
            {
                using (var command = new MySqlCommand(query.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@Id", userId);

                    try
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                user = new Client();
                                user.Id = Convert.ToInt16(reader["Id"]);
                                user.Name = Convert.ToString(reader["Name"]);
                                user.Password = Convert.ToString(reader["Password"]);
                                user.Address = new Address();
                                user.Address.Street = Convert.ToString(reader["Street"]);
                                user.Address.Number = Convert.ToInt16(reader["Number"]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }


            return user;

        }

        public Client GetUser2(short userId)
        {
            Client user = null;

            var result = _context.Client.Where(u => u.Id == userId).First();



            return user;

        }
    }
}