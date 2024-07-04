using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.LoginDtos
{
    public class UpdateAdminUsernameDto
    {
        public int AppUserID { get; set; }
        public string NewUsername { get; set; }
    }
}
