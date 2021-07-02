using Microsoft.AspNetCore.Identity;
using System;

namespace WebApp.DAL.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string AddressDelivery { get; set; }
    }
}
