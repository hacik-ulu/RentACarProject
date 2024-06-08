using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.ViewModels
{
    public class ReservationDetailsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }
        public int DriverLicenseYear { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PickUpLocationName { get; set; }
        public string DropOffLocationName { get; set; }
        public string CarBrandName { get; set; }
        public string CarModel { get; set; }
    }
}
