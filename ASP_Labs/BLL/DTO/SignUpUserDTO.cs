using System.ComponentModel.DataAnnotations;

namespace WebApp.BLL.DTO
{
    public class SignUpUserDTO
    {
        /// <summary>
        /// User Email
        /// </summary>
        /// <example>string@gmail.com</example>
        [Required, EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// User unique name
        /// </summary>
        /// <example>PolkovnikSDushoy</example>
        [Required, StringLength(32, MinimumLength = 6)]
        public string UserName { get; set; }

        /// <summary>
        /// User phone number
        /// </summary>
        /// <example>+375292929292</example>
        [Required, Phone]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// User address
        /// </summary>
        /// <example>Krasnaya polyana, 12/2/63</example>
        [Required, StringLength(150, MinimumLength = 5)]
        public string AddressDelivery { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        /// <example>HelloMyNameIsZuzi228</example>

        [Required, StringLength(32, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
