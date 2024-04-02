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

        public Client GetUser(short userId)
        {
           return _userRepository.GetUser(userId);
        }

        public Client GetUserComplete(short userId)
        {
            return _userRepository.GetUser(userId);
        }

    }
}