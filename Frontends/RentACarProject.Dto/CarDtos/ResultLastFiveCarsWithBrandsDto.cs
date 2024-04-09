using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.CarDtos
{
    public class ResultLastFiveCarsWithBrandsDto
    {
        public int CarID { get; set; }
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public string Model { get; set; }
        public string CoverImagerUrl { get; set; }
        public decimal Mileage { get; set; }
        public int Year { get; set; }
        public string Transmission { get; set; }
        public byte Seat { get; set; }
        public byte Luggage { get; set; }
        public string Fuel { get; set; }
        public string BigImageUrl { get; set; }
    }
}
