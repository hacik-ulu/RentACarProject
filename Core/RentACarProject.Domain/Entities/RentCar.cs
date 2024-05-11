using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Domain.Entities
{
    public class RentCar
    {
        public int RentCarID { get; set; }
        public int LocationID { get; set; }
        public Location Location { get; set; }
        public int CarID { get; set; }
        public Car Car { get; set; }
        public bool Available { get; set; }
    }
}
