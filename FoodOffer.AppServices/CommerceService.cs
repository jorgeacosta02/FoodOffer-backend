using FoodOffer.Infrastructure;
using FoodOffer.Model.Models;
using FoodOffer.Model.Repositories;
using FoodOffer.Model.Services;
using Microsoft.AspNetCore.Http;

namespace FoodOffer.AppServices
{
    public class CommerceService: ICommerceService
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly IImagesRepository _imagesRepository;
        private readonly ICommerceRepository _commerceRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly AmazonS3Service _s3Service;
        private static string bucketName = "clickfood";
        private readonly string s3Path = "https://s3.sa-east-1.amazonaws.com/clickfood/";
        public CommerceService(IAttributeRepository attributeRepository, 
            IImagesRepository imagesRepository,
            ICommerceRepository commerceRepository,
            IAddressRepository addressRepository, AmazonS3Service s3Service) 
        {
            _attributeRepository = attributeRepository ?? throw new ArgumentNullException(nameof(attributeRepository));
            _imagesRepository = imagesRepository ?? throw new ArgumentNullException(nameof(imagesRepository));
            _commerceRepository = commerceRepository ?? throw new ArgumentNullException(nameof(commerceRepository));
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _s3Service = s3Service ?? throw new ArgumentNullException(nameof(s3Service));
        }

        public List<Commerce> GetCommerces()
        {
            return _commerceRepository.GetCommerces();
        }

        public Commerce GetCommerce(int comID)
        {
            return _commerceRepository.GetCommerce(comID);
        }

        public Commerce GetCommerceComplete(int comID)
        {
            var commerce = _commerceRepository.GetCommerce(comID);

            if (commerce == null)
                return null;

            commerce.Addresses = _addressRepository.GetAddresses(comID, 'C');

            commerce.Attributes = _attributeRepository.GetCommerceAttributes(comID);

            return commerce;
        }

        public Commerce CompleteCommerceData(Commerce com) 
        {
            com.Addresses = _addressRepository.GetAddresses(com.Id, 'C');

            if (com == null)
                throw new Exception("Advertising Not - Found");


            return com;
        }

        public Commerce AddCommerce(Commerce commerce)
        {
            //Save commerce data to get an ID.
            commerce.Id = _commerceRepository.SaveCommerceData(commerce);

            if (commerce.Id == 0)
                throw new Exception("Error saving commerce data");

            return commerce;

        }

        public void SaveCommerceImage(int commerceId, IFormFile image)
        {
            if (image != null)
            {
                var commerce = GetCommerce(commerceId);

                if (commerce == null)
                    throw new Exception($"The commerce ID: {commerceId} wasn't found");

                commerce.Logo = new AppImage();
                commerce.Logo.ImageFile = image;
                commerce.Logo.Item = 1;
                commerce.Logo.Name = commerce.Name;

                string extension = Path.GetExtension(commerce.Logo.ImageFile.FileName);
                commerce.Logo.ReferenceId = commerce.Id;
                commerce.Logo.Name = commerce.Name;
                commerce.Logo.Path = $"logo/com_id_{commerce.Id}{extension}";

                //Upload image to S3 Bucket
                if (!_s3Service.UploadFileAsync(bucketName, commerce.Logo.Path, commerce.Logo.ImageFile).Result)
                    throw new Exception("Error uploading commerce image");

                //Save image data in database
                if (!_imagesRepository.SaveImageData(commerce.Logo, 'C'))
                    throw new Exception("Error saving commerce image data");

                commerce.Logo.Path = s3Path + commerce.Logo.Path;

                commerce.Logo.ImageFile = null;
            }

        }

        public bool UpdateCommerceImage(int commerceId, IFormFile image)
        {
            bool flag = false;

            var oldImage = _imagesRepository.GetCommerceImage(commerceId);

            if (oldImage != null)
            {
                if (!_s3Service.DeleteFileAsync(bucketName, oldImage.Path).Result)
                    throw new Exception("Error deleting image file from storage");

                if (!_imagesRepository.DeleteImageData(commerceId, 1))
                    throw new Exception("Error deleting advertising image data");
            }

            SaveCommerceImage(commerceId, image);

            flag = true;

            return flag;
        }


    }
}
