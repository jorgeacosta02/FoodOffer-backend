using FoodOffer.Infrastructure;
using FoodOffer.Model.Models;
using FoodOffer.Model.Repositories;
using FoodOffer.Model.Services;

namespace FoodOffer.AppServices
{
    public class AdvertisingService: IAdvertisingService
        // Trae los datos del usuario relacionado al aviso.

    {
        private readonly IAdvertisingRepository _advertisingRepository;
        //private readonly AmazonS3Service _s3Service;
        public AdvertisingService(IAdvertisingRepository advertisingRepository) 
        {
            _advertisingRepository = advertisingRepository ?? throw new ArgumentNullException(nameof(advertisingRepository));
            //_s3Service = s3Service ?? throw new ArgumentNullException(nameof(s3Service));
        }

        public List<Advertising> GetAdvertisings(Filter filter)
        {
            return _advertisingRepository.GetAdvertisings(filter);
        }

        public Advertising GetAdvertising(int Id) 
        {
            Advertising advertising = new Advertising();
            ////GPG marca un errror al utilizar el dato de advertising.Id antes de asignarle un valor.
            //advertising.us = _userRepository.GetUser((short)advertising.Id);
            ////advertising.Properties.AddRange
            return advertising;
        }

    }
}
