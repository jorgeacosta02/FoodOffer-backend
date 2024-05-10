using FoodOffer.Infrastructure;
using FoodOffer.Model.Models;
using FoodOffer.Model.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
// using Microsoft.EntityFrameworkCore;, using Microsoft.Extensions.Options;: Estos using importan los espacios de nombres necesarios para trabajar con Entity Framework Core y las opciones de configuración.
using MySql.Data.MySqlClient; // Este using importa el espacio de nombres necesario para trabajar con MySQL utilizando ADO.NET.
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using MySqlX.XDevAPI;
using FoodOffer.Infrastructure.DbModels;

// Acá se hacen las solicitudes a la base de datos. Una ventaja es poder hacer una sola solicitud con uniones
// con diferentes tablas relacionadas. Este repository puede ser usado por diferentes services para 
// extraer la info necesaria.

// using System.Drawing;, using static System.Net.Mime.MediaTypeNames;: Estos using no parecen ser necesarios y podrían ser eliminados.

namespace FoodOffer.Repository
{
    public class UserRepository : IUserRepository // Interface del archivo FoodOffer.Model.Repositories que tiene los métodos Client GetUser y Client GetUser2
    {
        private readonly ApplicationDbContext _context;
        // Esta es una instancia del contexto de la base de datos ApplicationDbContext. Se utiliza para interactuar con la base de datos utilizando Entity Framework Core. 
        // AplicationDbContext es una clase que hereda de DbContex.
        private readonly IDbConecction session;
        
        public UserRepository(IDbConecction dbConecction, ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            session = dbConecction ?? throw new ArgumentNullException(nameof(dbConecction));
        }


        public User GetUser(short userId)      
        {
            User user = null;

            var query = new StringBuilder();
            query.AppendLine("SELECT * FROM client ");
            query.AppendLine("LEFT JOIN address ON Client_Id = Id ");
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
                                user = new User();
                                user.Id = Convert.ToInt16(reader["Id"]);
                                user.Name = Convert.ToString(reader["Name"]);
                                user.Password = Convert.ToString(reader["Password"]);
                                user.Email = Convert.ToString(reader["Email"]);
                                user.Address = new Model.Models.Address();
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

        public List <User> GetUsers()
        {
            List <User> users = new List<User>();

            var query = new StringBuilder();
            query.AppendLine("SELECT * FROM client ");
            query.AppendLine("JOIN adress ON Client_Id = Id ");


            using (var connection = session.GetConnection())
            {
                using (var command = new MySqlCommand(query.ToString(), connection))
                {

                    try
                    {

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var user = new User();
                                user.Id = Convert.ToInt16(reader["Id"]);
                                user.Name = Convert.ToString(reader["Name"]);
                                user.Password = Convert.ToString(reader["Password"]);
                                user.Address = new Model.Models.Address();
                                user.Address.Street = Convert.ToString(reader["Street"]);
                                user.Address.Number = Convert.ToInt16(reader["Number"]);
                                users.Add(user);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }


            return users;

        }

        public bool CreateUser(User data)
        {
            var flag = false;
            try
            {
                var query = new StringBuilder();
                query.AppendLine("INSERT INTO client (Name, Password, Email) VALUES (?, ?, ?)");


                using (var connection = session.GetConnection())
                {
                    using (var command = new MySqlCommand(query.ToString(), connection))
                    {

                        command.Parameters.AddWithValue("@Name", data.Name);
                        command.Parameters.AddWithValue("@Password", data.Password);
                        command.Parameters.AddWithValue("@Email", data.Email);

                        connection.Open();

                        flag = command.ExecuteNonQuery() == 1 ? true:false;

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving the advertising image data.", ex);
            }

            return flag;

        }

        public User InsertUser(User data)
        {
            var usr = new ClientDb();
            usr.Name = data.Name;
            usr.Password = data.Password;
            usr.Email = data.Email;

            var result = _context.Client.Add(usr);

            _context.SaveChanges();
            data.Id = usr.Id;

            return data;
        }

        public User GetUser2(short userId)
        {
            User user = null;

            var result = _context.Client.Where(u => u.Id == userId).First();



            return user;

        }
    }
}