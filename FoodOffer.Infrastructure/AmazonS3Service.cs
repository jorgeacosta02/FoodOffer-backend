using Amazon.Runtime;
using Amazon.S3.Transfer;
using Amazon.S3;
using Microsoft.Extensions.Options;
using FoodOffer.Model.Models;
using Microsoft.AspNetCore.Http;

namespace FoodOffer.Infrastructure
{
    public class AmazonS3Service
    {
        public string AWSKeyId { get; private set; }
        public string AWSKeySecret { get; private set; }
        public BasicAWSCredentials Credentials { get; private set; }

        private readonly IAmazonS3 _amazonS3;

        public AmazonS3Service(IOptions<AWSOptions> options)
        {
            AWSKeyId = options.Value.KeyId;
            AWSKeySecret = options.Value.KeySecret;
            Credentials = new BasicAWSCredentials(AWSKeyId, AWSKeySecret);
            var config = new AmazonS3Config();
            config.RegionEndpoint = Amazon.RegionEndpoint.SAEast1;
            _amazonS3 = new AmazonS3Client(Credentials, config);
        }

        public async Task<bool> UploadFileAsync(string bucketName, string key, IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);

            var fileTransferUtility = new TransferUtility(_amazonS3);

            await fileTransferUtility.UploadAsync(new TransferUtilityUploadRequest
            {
                InputStream = memoryStream,
                Key = key,
                BucketName = bucketName,
                ContentType = file.ContentType
            });

            return true;

        }

    }
}
