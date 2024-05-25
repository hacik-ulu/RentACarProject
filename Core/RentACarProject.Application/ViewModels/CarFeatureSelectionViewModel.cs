using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.ViewModels
{
    public class CarFeatureSelectionViewModel
    {
        public int CarId { get; set; }
        public List<FeatureSelectionDto> Features { get; set; }
    }

    public class FeatureSelectionDto
    {
        public int FeatureID { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }  
    }
}
