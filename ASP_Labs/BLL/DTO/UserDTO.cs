using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.BLL.DTO
{
    public class UserDTO
    {
        /// <summary>
        /// User unique identifier
        /// </summary>
        /// <example>01111d6e-ed62-482b-a4d9-335dfa68d58e</example>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// User unique name
        /// </summary>
        /// <example>Zuzi139</example>
        [Required, StringLength(100, MinimumLength = 6)]
        public string UserName { get; set; }

        /// <summary>
        /// Concurrency Stamp
        /// </summary>
        /// <example>a1f4ba72-a222-2222-22cb-f8db64e71d23</example>
        [Required]
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// User phone number
        /// </summary>
        /// <example>+1212682539215555</example>
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// User delivery address
        /// </summary>
        /// <example>Ulica Razbitih fonarey, 138</example>
        [Required]
        public string AddressDelivery { get; set; }

    }
}
