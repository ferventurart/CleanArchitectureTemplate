using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using ApplicationCore.Interfaces;
using Infraestructure.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infraestructure.Services
{
    public class WebFileSystem : IFileSystem
    {
        private readonly IAppLogger<WebFileSystem> _logger;

        public WebFileSystem(IAppLogger<WebFileSystem> logger)
        {
            _logger = logger;
        }

        public async Task<string> SaveImage(Stream stream, string fileName, string bucketName)
        {
            try
            {
                var client = new AmazonS3Client(
                    "", 
                    "", 
                    RegionEndpoint.USEast1
                );

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = stream,
                    Key = fileName,
                    BucketName = bucketName,
                    CannedACL = S3CannedACL.PublicRead
                };

                var fileTransferUtility = new TransferUtility(client);
                await fileTransferUtility.UploadAsync(uploadRequest);

                return GetUrlS3(bucketName, fileName);

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return null;
            }

        }
    
        private string GetUrlS3(string bucketName, string fileName)
        {
            return $"https://{bucketName}.s3.amazonaws.com/{fileName}";
        }
    }
}
