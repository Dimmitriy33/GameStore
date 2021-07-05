using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;
using WebApp.Web.Startup.Settings;

namespace WebApp.BLL.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly AppSettings _appSettings;
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(AppSettings appSettings)
        {
            _appSettings = appSettings;
            _cloudinary = new Cloudinary(CloudinaryAccount);
            _cloudinary.Api.Secure = true;
        }

        private Account CloudinaryAccount => new Account(
                _appSettings.CloudinarySettings.CloudName,
                _appSettings.CloudinarySettings.ApiKey,
                _appSettings.CloudinarySettings.ApiSecret
                );

        public async Task<string> UploadImage(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = _appSettings.CloudinarySettings.DefaultCloudinaryFolder
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult.SecureUrl.AbsoluteUri.ToString();
        }
    }
}
