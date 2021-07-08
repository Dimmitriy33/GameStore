using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebApp.BLL.Interfaces
{
    public interface ICloudinaryService
    {
        Task<string> UploadImage(IFormFile file);
    }
}
