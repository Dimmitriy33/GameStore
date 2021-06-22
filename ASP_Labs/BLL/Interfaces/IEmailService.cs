using System.Threading.Tasks;

namespace WebApp.BLL.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string email, string subject, string message);
    }
}
