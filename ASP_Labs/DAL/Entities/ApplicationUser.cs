using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace WebApp.DAL.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string AddressDelivery { get; set; }
        public ICollection<ProductRating> Ratings { get; set; }

    }
}
