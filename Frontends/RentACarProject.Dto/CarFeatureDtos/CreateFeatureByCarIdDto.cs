using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.CarFeatureDtos
{
    public class CreateFeatureByCarIdDto
    {
        public int CarID { get; set; }
        public List<int> FeatureIDs { get; set; }
    }
}
