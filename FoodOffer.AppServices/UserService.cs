using FoodOffer.Model.Models;
using FoodOffer.Model.Repositories;
using FoodOffer.Model.Services;

namespace FoodOffer.AppServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
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

        public User PostUser(User data)
        {
            //if (_userRepository.CreateUser(data))
            //{
            //    return data;
            //}
            //return null;

            return _userRepository.InsertUser(data);
        }

    }
}