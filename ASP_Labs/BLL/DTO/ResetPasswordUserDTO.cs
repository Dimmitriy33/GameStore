using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.BLL.DTO
{
    public class ResetPasswordUserDTO
    {
        /// <summary>
        /// User unique identifier
        /// </summary>
        /// <example>01111d6e-ed62-482b-a4d9-335dfa68d58e</example>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Current user password
        /// </summary>
        /// <example>HelloMyNameIsZuzi228</example>
        [Required]
        public string OldPassword { get; set; }

        /// <summary>
        /// New user password
        /// </summary>
        /// <example>HelloMyNameIsZuzi139</example>
        [Required, StringLength(100, MinimumLength = 6)]
        public string NewPassword { get; set; }
    }
}
