using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.CarDescriptionDtos
{
    public class ResultCarDescriptionDto
    {
        public int CarDescriptionID { get; set; }
        public int CarID { get; set; }
        public List<string> CarDetails { get; set; }

        // Tek bir string olarak tutacak özellik.
        public string CarDetailsAsString => string.Join(", ", CarDetails);
    }
}
