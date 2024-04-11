using FoodOffer.Model.Models;
using FoodOffer.Model.Repositories;
using FoodOffer.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.AppServices
{
    public class AdvertisingService: IAdvertisingService

    {
        private readonly IUserRepository _userRepository;
        public AdvertisingService(IUserRepository userRepository) {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public Advertising GetAdvertising(int Id) {
            Advertising advertising = new Advertising();
            advertising.ClientData = _userRepository.GetUser((short)advertising.Id);
            advertising.Properties.AddRange
            return advertising;
        }

    }
}
