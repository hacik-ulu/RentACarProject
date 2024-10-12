using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.SignUpDtos
{
    public class ChangeMemberPasswordDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(30, ErrorMessage = "Password must be between 6 and 30 characters long.", MinimumLength = 6)]
        public string? NewPassword { get; set; }
    }
}
