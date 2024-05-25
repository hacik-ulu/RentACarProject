using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Interfaces.CarFeatureInterfaces
{
    public interface ICarFeatureRepository
    {
        Task<List<CarFeature>> GetCarFeaturesByCarIDAsync(int carID);
        void ChangeCarFeatureAvailabilityToFalse(int id);
        void ChangeCarFeatureAvailabilityToTrue(int id);
        void CreateCarFeatureByCar(CarFeature carFeature);
    }
}
