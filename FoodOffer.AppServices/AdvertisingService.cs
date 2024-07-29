using FoodOffer.Infrastructure;
using FoodOffer.Model.Models;
using FoodOffer.Model.Repositories;
using FoodOffer.Model.Services;
using static System.Net.Mime.MediaTypeNames;

namespace FoodOffer.AppServices
{
    public class AdvertisingService: IAdvertisingService
    {
        private readonly IAdvertisingRepository _advertisingRepository;
        private readonly IImagesRepository _imagesRepository;
        private readonly ICommerceRepository _commerceRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IAttributeRepository _attributeRepository;
        private readonly string s3Path = "https://s3.sa-east-1.amazonaws.com/clickfood/";
        private readonly AmazonS3Service _s3Service;
        private static string bucketName = "clickfood";
        public AdvertisingService(IAdvertisingRepository advertisingRepository, 
            IImagesRepository imagesRepository, 
            ICommerceRepository commerceRepository,
            IAddressRepository addressRepository, 
            IAttributeRepository attributeRepository,
            AmazonS3Service s3Service) 
        {
            _advertisingRepository = advertisingRepository ?? throw new ArgumentNullException(nameof(advertisingRepository));
            _imagesRepository = imagesRepository ?? throw new ArgumentNullException(nameof(imagesRepository));
            _commerceRepository = commerceRepository ?? throw new ArgumentNullException(nameof(commerceRepository));
            _s3Service = s3Service ?? throw new ArgumentNullException(nameof(s3Service));
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _attributeRepository = attributeRepository ?? throw new ArgumentNullException(nameof(attributeRepository));
        }

        public List<Advertising> GetAdvertisings(AdvFilter filter)
        {
            return _advertisingRepository.GetAdvertisings(filter);
        }

        public Advertising GetAdvertising(int Id) 
        {
            Advertising adv = _advertisingRepository.GetAdvertising(Id);

            if (adv == null)
                throw new Exception("Advertising Not - Found");

            adv.Commerce = _commerceRepository.GetCommerce(adv.Commerce.Id);

            adv.Commerce.Addresses = _addressRepository.GetAdvertisingAddresses(adv.Id);

            adv.Attributes = _attributeRepository.GetAdvertisingsAttribute(adv.Id);

            adv.Images.Add(_imagesRepository.GetAdvertisingImage(adv.Id));

            foreach(var img in adv.Images)
            {
                img.Path = s3Path + img.Path;
            }


            return adv;
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

        public Advertising UpdateAdvertising(Advertising advertising)
        {

            if (!_advertisingRepository.UpdateAdvertisingData(advertising))
                throw new Exception("Error updating advertising data");


            if(advertising.Images.Count > 0)
            {
                foreach (var img in advertising.Images)
                {

                    if(img.New)
                    {
                        string extension = Path.GetExtension(img.ImageFile.FileName);
                        img.ReferenceId = advertising.Id;
                        img.Name = advertising.Title;
                        img.Path = $"com_id_{advertising.Commerce.Id}/adv_id_{advertising.Id}-adi_item_{img.Item}{extension}";

                        if (!_s3Service.UploadFileAsync(bucketName, img.Path, img.ImageFile).Result)
                            throw new Exception("Error saving advertising image");

                        if (!_imagesRepository.SaveImageData(img, 'A'))
                            throw new Exception("Error saving advertising image data");
                    }

                    if (!img.Keep)
                    {
                        if (!_s3Service.DeleteFileAsync(bucketName, img.Path).Result)
                            throw new Exception("Error deleting image file from storage");

                        if (!_imagesRepository.DeleteImageData(advertising.Id, img.Item))
                            throw new Exception("Error deleting advertising image data");
                    }

                }
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
