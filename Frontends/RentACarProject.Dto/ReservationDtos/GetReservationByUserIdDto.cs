using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.ReservationDtos
{
    public class GetReservationByUserIdDto
    {
        public int ReservationID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PickUpLocationName { get; set; }
        public string DropOffLocationName { get; set; }
        public int CarID { get; set; }
        public string Description { get; set; }
        public string CarModel { get; set; }
        public string BrandName { get; set; }
    }
}
