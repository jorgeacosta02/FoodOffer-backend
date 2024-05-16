using FoodOffer.Model.Models;
using FoodOffer.Model.Repositories;
using FoodOffer.Model.Services;
using System.Security.Cryptography;
using System.Text;

namespace FoodOffer.AppServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoginRepository _loginRepository;

        public UserService(IUserRepository userRepository, ILoginRepository loginRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _loginRepository = loginRepository ?? throw new ArgumentNullException(nameof(loginRepository));
        }

        public User GetUser(short userId)
        {
           return _userRepository.GetUser(userId);
        }
        public List<User> GetUsers(short userId)
        {
            return _userRepository.GetUsers();
        }


        public User GetUserComplete(short userId)
        {
            return _userRepository.GetUser(userId);
        }

        public User Login(LoginData data)
        {
            var user = _userRepository.GetUserCompleteByEmail(data.Email);

            if (user == null)
                throw new Exception("User email not found");

            var loginData = _loginRepository.GetAccess(user.Id_User);

            if (loginData == null)
                throw new Exception("Access data not found");

            if(!VerifyPassword(data.Password, loginData))          
                throw new Exception("Wrong password");


            return user;
        }

        public User CreateUser(User user)
        {
            var userExist = _userRepository.GetUserByEmail(user.Email);
            
            if (userExist != null)
                throw new Exception("User email already exists");

            user = _userRepository.InsertUser(user);

            if(user != null && user.Id_User != 0) 
            {

                string salt = GenerateSalt();

                string hashedPassword = HashPassword(user.Password, salt);

                user.Password = hashedPassword;

                _loginRepository.CreateLoginData(user, salt);

                user.Password = null;
            }

            return user;

        }

        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        public static string HashPassword(string password, string salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] combinedBytes = new byte[passwordBytes.Length + saltBytes.Length];

            Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
            Buffer.BlockCopy(saltBytes, 0, combinedBytes, passwordBytes.Length, saltBytes.Length);

            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(combinedBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public bool VerifyPassword(string userPassword, LoginData dbData)
        {
            string hashedPassword = HashPassword(userPassword, dbData.Salt);

            return dbData.Password == hashedPassword;
        }

    }
}