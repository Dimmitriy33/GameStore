using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.BLL.DTO
{
    public class UserDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string ConcurrencyStamp { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string AddressDelivery { get; set; }

    }
}
