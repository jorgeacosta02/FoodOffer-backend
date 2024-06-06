using FoodOffer.Infrastructure;
using FoodOffer.Model.Models;
using FoodOffer.Model.Repositories;
// using Microsoft.EntityFrameworkCore;, using Microsoft.Extensions.Options;: Estos using importan los espacios de nombres necesarios para trabajar con Entity Framework Core y las opciones de configuración.
using MySql.Data.MySqlClient; // Este using importa el espacio de nombres necesario para trabajar con MySQL utilizando ADO.NET.
using System.Text;
using clasificados.Infraestructure.DbContextConfig.DbModels;
using AutoMapper;

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
        private readonly IDbConecction _session;
        private readonly IMapper _mapper;
        
        public UserRepository(IDbConecction dbConecction, ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _session = dbConecction ?? throw new ArgumentNullException(nameof(dbConecction));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #region Queries

        public User GetUser(short userId)      
        {
            User user = null;

            var query = new StringBuilder();
            query.AppendLine("SELECT * FROM client ");
            query.AppendLine("LEFT JOIN address ON Client_Id = Id ");
            query.AppendLine("WHERE Id = ? ");


            using (var connection = _session.GetConnection())
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
                                user.Id_User = Convert.ToInt16(reader["Id"]);
                                user.Name = Convert.ToString(reader["Name"]);
                                user.Password = Convert.ToString(reader["Password"]);
                                user.Email = Convert.ToString(reader["Email"]);
                                user.Address = new Model.Models.Address();
                                //user.Address.Street = Convert.ToString(reader["Street"]);
                                //user.Address.Number = Convert.ToInt16(reader["Number"]);
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

        public User GetUserCompleteByEmail(string email)
        {
            User user = null;

            var query = new StringBuilder();
            query.AppendLine("SELECT * FROM users ");
            query.AppendLine("LEFT JOIN addresses ON usr_id = add_ref_id AND add_ref_type = 'U' ");
            query.AppendLine("INNER JOIN countries ON cou_cod = add_cou_cod ");
            query.AppendLine("INNER JOIN states ON ste_cod = add_ste_cod AND ste_cou_cod = add_cou_cod ");
            query.AppendLine("INNER JOIN cities ON cit_cod = add_cit_cod AND cit_cou_cod = add_cou_cod AND cit_ste_cod == add_ste_cod ");
            query.AppendLine("WHERE usr_mail = ? ");


            using (var connection = _session.GetConnection())
            {
                using (var command = new MySqlCommand(query.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@usr_mail", email);

                    try
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                user = new User();
                                user.Id_User = Convert.ToInt16(reader["usr_id"]);
                                user.Name = Convert.ToString(reader["usr_name"]);
                                user.Email = Convert.ToString(reader["usr_mail"]);
                                user.Address = new Address();
                                user.Address.Description = Convert.ToString(reader["add_desc"]);
                                user.Address.City = new City(Convert.ToInt16(reader["cit_cod"]), Convert.ToString(reader["cit_desc"]));
                                user.Address.State = new State(Convert.ToInt16(reader["ste_cod"]), Convert.ToString(reader["ste_desc"]));
                                user.Address.Country = new Country(Convert.ToInt16(reader["cou_cod"]), Convert.ToString(reader["cou_desc"]));
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

        public User GetUserByIdSimple(short userId)
        {
            User user = null;

            var result = _context.users.Where(u => u.usr_id == userId).First();

            return user;

        }

        public User GetUserByEmail(string email)
        {
            User user = null;

            var result = _context.users.FirstOrDefault(u => u.usr_mail == email);

            if(result != null)
                user = _mapper.Map<User>(result);

            return user;

        }


        public List <User> GetUsers()
        {
            List <User> users = new List<User>();

            var query = new StringBuilder();
            query.AppendLine("SELECT * FROM client ");
            query.AppendLine("JOIN adress ON Client_Id = Id ");


            using (var connection = _session.GetConnection())
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
                                user.Id_User = Convert.ToInt16(reader["Id"]);
                                user.Name = Convert.ToString(reader["Name"]);
                                //user.Password = Convert.ToString(reader["Password"]);
                                //user.Address = new Model.Models.Address();
                                //user.Address.Street = Convert.ToString(reader["Street"]);
                                //user.Address.Number = Convert.ToInt16(reader["Number"]);
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


                using (var connection = _session.GetConnection())
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

        #endregion

        #region Transactions

        public User InsertUser(User user)
        {
            var usr = _mapper.Map<Db_User>(user);

            var result = _context.users.Add(usr);
            _context.SaveChanges();

            user.Id_User = usr.usr_id;

            return user;
        }

        public User UpdateUser(User user)
        {
            var dbUser = _mapper.Map<Db_User>(user);

            var existingUser = _context.users.Find(dbUser.usr_id);

            if (existingUser != null)
            {
                _mapper.Map(user, existingUser);
                _context.SaveChanges();
                return user;
            }
            else
            {
                throw new Exception("User not found.");
            }
        }

        public void DeleteUser(int userId)
        {
            var userToDelete = _context.users.Find(userId);

            if (userToDelete != null)
            {
                _context.users.Remove(userToDelete);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("User not found.");
            }
        }

        #endregion


    }
}