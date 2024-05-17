using RentACarProject.Application.ViewModels;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Interfaces.CarPricingInterfaces
{
	public interface ICarPricingRepository
	{
		Task<List<CarPricing>> GetCarPricingWithCarsAsync();
		Task<List<CarPricingViewModel>> GetCarPricingWithTimePeriodAsync();

	}
}
