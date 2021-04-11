using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IFileSystem _fileSystem;
        private readonly IAppLogger<FileUploadService> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploadService(IFileSystem fileSystem, IAppLogger<FileUploadService> logger, IWebHostEnvironment webHostEnvironment)
        {
            _fileSystem = fileSystem;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> SaveFileOnAWSS3(IFormFile file, string fileName, string bucketName)
        {
            try
            {
                var urlFotografia = string.Empty;
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    urlFotografia = await _fileSystem.SaveImage(memoryStream, fileName, bucketName);
                }
                return string.IsNullOrEmpty(urlFotografia) ? null : urlFotografia;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return null;
            }
        }

        public async Task<string> SaveFileOnDisk(IFormFile file, string fileName, string folderDestination)
        {
            try
            {
                if (file == null)
                    return null;

                FileInfo fileInfo = new FileInfo(file.FileName);
                var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\uploads\\{folderDestination}";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, $"uploads\\{folderDestination}", $"{fileName}{fileInfo.Extension}");

                var memoryStream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(memoryStream);

                if (!Directory.Exists(folderDirectory))
                {
                    Directory.CreateDirectory(folderDirectory);
                }

                await using(var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.WriteTo(fs);
                    memoryStream.WriteTo(fs);
                }

                return $"https://localhost:44356/uploads/" + $"{folderDestination}/{fileName}{fileInfo.Extension}";
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return null;
            }
        }
    }
}
