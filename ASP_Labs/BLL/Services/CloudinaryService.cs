using CloudinaryDotNet;
using WebApp.BLL.Interfaces;
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
    }
}
