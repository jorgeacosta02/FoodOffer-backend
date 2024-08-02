using FoodOffer.Infrastructure;
using FoodOffer.Model.Models;
using FoodOffer.Model.Repositories;
using FoodOffer.Model.Services;
using static System.Net.Mime.MediaTypeNames;

namespace FoodOffer.AppServices
{
    public class CommerceService: ICommerceService
    {
        private readonly IAdvertisingRepository _advertisingRepository;
        private readonly IImagesRepository _imagesRepository;
        private readonly ICommerceRepository _commerceRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly AmazonS3Service _s3Service;
        private static string bucketName = "clickfood";
        public CommerceService(IAdvertisingRepository advertisingRepository, 
            IImagesRepository imagesRepository,
            ICommerceRepository commerceRepository,
            IAddressRepository addressRepository, AmazonS3Service s3Service) 
        {
            _advertisingRepository = advertisingRepository ?? throw new ArgumentNullException(nameof(advertisingRepository));
            _imagesRepository = imagesRepository ?? throw new ArgumentNullException(nameof(imagesRepository));
            _commerceRepository = commerceRepository ?? throw new ArgumentNullException(nameof(commerceRepository));
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _s3Service = s3Service ?? throw new ArgumentNullException(nameof(s3Service));
        }

        public Commerce GetCommerce(int comID)
        {
            return _commerceRepository.GetCommerce(comID);
        }

        public Commerce CompleteCommerceData(Commerce com) 
        {
            com.Addresses = _addressRepository.GetAddresses(com.Id, 'C');

            if (com == null)
                throw new Exception("Advertising Not - Found");


            return com;
        }

        public Advertising CreateAdvertising(Advertising advertising)
        {
            advertising.Id = _advertisingRepository.SaveAdvertisingData(advertising);

            if (advertising.Id == 0)
                throw new Exception("Error saving advertising data");

            foreach(var img in advertising.Images) 
            {
                string extension = Path.GetExtension(img.ImageFile.FileName);
                img.ReferenceId = advertising.Id;
                img.Name = advertising.Title;
                img.Path = $"com_id_{advertising.Commerce.Id}/adv_id_{advertising.Id}-adi_item_{img.Item}{extension}";
                if(!_s3Service.UploadFileAsync(bucketName, img.Path, img.ImageFile).Result)
                    throw new Exception("Error saving advertising image");

                if(!_imagesRepository.SaveImageData(img, 'A'))
                    throw new Exception("Error saving advertising image data");

                img.ImageFile = null;
            }

            return advertising;
        }

     

        public bool UpdateAdvertisingState(Advertising advertising)
        {
            return _advertisingRepository.UpdateAdvertisingState(advertising);
        }

         public bool DeleteAdvertising(int id)
        {
            return _advertisingRepository.DeleteAdvertisingData(id);
        }
    }
}
