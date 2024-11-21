using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.SignUpDtos
{
    public class UpdateMemberUsernameDto
    {
        [Required(ErrorMessage ="UserID is required.")]
        public int AppUserID { get; set; }

        [Required(ErrorMessage = "User name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string NewUsername { get; set; }
    }
}
