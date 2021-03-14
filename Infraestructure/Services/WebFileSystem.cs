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

        public async Task<bool> SavePicture(string pictureName, string pictureBase64, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(pictureBase64) || !await UploadFile(pictureName, Convert.FromBase64String(pictureBase64), cancellationToken))
            {
                return false;
            }

            return true;
        }

        private async Task<bool> UploadFile(string fileName, byte[] fileData, CancellationToken cancellationToken)
        {
            if (!fileData.IsValidImage(fileName))
            {
                return false;
            }

            return await UploadToWeb(fileName, fileData, cancellationToken);
        }

        private async Task<bool> UploadToWeb(string fileName, byte[] fileData, CancellationToken cancellationToken)
        {
            var request = new FileItem
            {
                DataBase64 = Convert.ToBase64String(fileData),
                FileName = fileName
            };
            //var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            //using var message = await _httpClient.PostAsync(_url, content, cancellationToken);
            //if (!message.IsSuccessStatusCode)
            //{
            //    return false;
            //}

            return true;
        }
    }

    public static class ImageValidators
    {
        private const int ImageMaximumBytes = 512000;

        public static bool IsValidImage(this byte[] postedFile, string fileName)
        {
            return postedFile != null && postedFile.Length > 0 && postedFile.Length <= ImageMaximumBytes && IsExtensionValid(fileName);
        }

        private static bool IsExtensionValid(string fileName)
        {
            var extension = Path.GetExtension(fileName);

            return string.Equals(extension, ".jpg", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(extension, ".png", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(extension, ".gif", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(extension, ".jpeg", StringComparison.OrdinalIgnoreCase);
        }
    }
}
