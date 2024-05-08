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
        // Trae los datos del usuario relacionado al aviso.

    {
        private readonly IUserRepository _userRepository;
        public AdvertisingService(IUserRepository userRepository) 
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public Advertising GetAdvertising(int Id) 
        {
            Advertising advertising = new Advertising();
            //GPG marca un errror al utilizar el dato de advertising.Id antes de asignarle un valor.
            advertising.ClientData = _userRepository.GetUser((short)advertising.Id);
            //advertising.Properties.AddRange
            return advertising;
        }

    }
}
