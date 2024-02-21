using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Domain.Entities;

public class Feature
{
    // Araba Genel Özellikleri
    public int FeatureID { get; set; }
    public string Name { get; set; }
    public List<CarFeature> CarFeatures { get; set; }

}
