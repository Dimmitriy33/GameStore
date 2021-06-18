using Microsoft.AspNetCore.Identity;

namespace WebApp.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string AddressDelivery { get; set; }
        public string Token { get; set; }

    }
}