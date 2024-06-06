using Amazon.Runtime;
using Amazon.S3.Transfer;
using Amazon.S3;
using Microsoft.Extensions.Options;
using FoodOffer.Model.Models;
using Microsoft.AspNetCore.Http;
using Amazon.S3.Model;

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

        public async Task<bool> DeleteFileAsync(string bucketName, string key)
        {
            try
            {
                var uri = new Uri(key);
                key = uri.AbsolutePath.Substring(1);

                var deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = key
                };

                var response = await _amazonS3.DeleteObjectAsync(deleteObjectRequest);

                if (response.HttpStatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (AmazonS3Exception e)
            {
                throw new Exception($"Error encountered on server. Message:'{e.Message}' when deleting an object");
            }
            catch (Exception e)
            {
                throw new Exception($"Error on internal server when deleting an image:'{e.Message}' ", e);
            }
        }

    }
}
