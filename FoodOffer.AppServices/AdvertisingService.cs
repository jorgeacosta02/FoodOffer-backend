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
        private readonly AmazonS3Service _s3Service;
        private static string bucketName = "clickfood";
        public AdvertisingService(IAdvertisingRepository advertisingRepository, IImagesRepository imagesRepository, AmazonS3Service s3Service) 
        {
            _advertisingRepository = advertisingRepository ?? throw new ArgumentNullException(nameof(advertisingRepository));
            _imagesRepository = imagesRepository ?? throw new ArgumentNullException(nameof(imagesRepository));
            _s3Service = s3Service ?? throw new ArgumentNullException(nameof(s3Service));
        }

        public List<Advertising> GetAdvertisings(Filter filter)
        {
            return _advertisingRepository.GetAdvertisings(filter);
        }

        public Advertising GetAdvertising(int Id) 
        {
            Advertising advertising = new Advertising();
            return advertising;
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
            }

            return advertising;
        }

    }
}
