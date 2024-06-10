using Microsoft.EntityFrameworkCore;
using RentACarProject.Application.Interfaces.CarPricingInterfaces;
using RentACarProject.Application.ViewModels;
using RentACarProject.Domain.Entities;
using RentACarProject.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Persistence.Repositories.CarPricingRepositories
{
	public class CarPricingRepository : ICarPricingRepository
	{
		private readonly RentACarContext _context;

		public CarPricingRepository(RentACarContext context)
		{
			_context = context;
		}

		public async Task<List<CarPricing>> GetCarPricingWithCarsAsync()
		{
			var values = await _context.CarPricings.Include(x => x.Car).ThenInclude(y => y.Brand).Include(x => x.Pricing).Where(z => z.PricingID == 1).ToListAsync();
			return values;
		}
		public async Task<List<CarPricingViewModel>> GetCarPricingWithTimePeriodAsync()
		{
			List<CarPricingViewModel> values = new List<CarPricingViewModel>();
			await using (var command = _context.Database.GetDbConnection().CreateCommand())
			{
				command.CommandText = @"
            Select * From 
            (
                Select 
                    Model, 
                    Name, 
                    CoverImagerUrl, 
                    PricingID, 
                    Amount 
                From 
                    CarPricings 
                Inner Join 
                    Cars On Cars.CarID = CarPricings.CarID 
                Inner Join 
                    Brands On Brands.BrandID = Cars.BrandID
            ) As SourceTable 
            Pivot 
            (
                Sum(Amount) For PricingID In ([1],[3],[4])
            ) as PivotTable;";
				command.CommandType = System.Data.CommandType.Text;

				await _context.Database.OpenConnectionAsync();
				try
				{
					await using (var reader = await command.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							CarPricingViewModel carPricingViewModel = new CarPricingViewModel
							{
                                Brand = reader["Name"].ToString(),
								Model = reader["Model"].ToString(),
								CoverImagerUrl = reader["CoverImagerUrl"].ToString(),
								Amounts = new List<decimal>
						{
							reader["1"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["1"]),
							reader["3"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["3"]),
							reader["4"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["4"])
						}
							};
							values.Add(carPricingViewModel);
						}
					}
				}
				finally
				{
					await _context.Database.CloseConnectionAsync();
				}
			}

			return values;
		}

	}
}
