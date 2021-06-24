using System.ComponentModel.DataAnnotations;

namespace WebApp.BLL.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string ConcurrencyStamp { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string AddressDelivery { get; set; }

    }
}
