using Microsoft.EntityFrameworkCore;
using RentACarProject.Application.Interfaces.StatisticsInterfaces;
using RentACarProject.Domain.Entities;
using RentACarProject.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Persistence.Repositories.StatisticsRepositories
{
    public class StatisticRepository : IStatisticsRepository
    {
        private readonly RentACarContext _context;

        public StatisticRepository(RentACarContext context)
        {
            _context = context;
        }

        public async Task<int> GetAuthorCountAsync()
        {
            var value = await _context.Authors.CountAsync();
            return value;
        }
        public async Task<decimal> GetAvgRentPriceForHourlyAsync()
        {
            int id = await _context.Pricings.Where(x => x.Name == "Hourly").Select(y => y.PricingID).FirstOrDefaultAsync();
            var value = await _context.CarPricings.Where(w => w.PricingID == id).AverageAsync(x => x.Amount);
            return value;
        }
        public async Task<decimal> GetAvgRentPriceForPerDayAsync()
        {
            int id = await _context.Pricings.Where(x => x.Name == "Per Day").Select(y => y.PricingID).FirstOrDefaultAsync();
            var value = await _context.CarPricings.Where(w => w.PricingID == id).AverageAsync(x => x.Amount);
            return value;
        }
        public async Task<decimal> GetAvgRentPriceForWeeklyAsync()
        {
            int id = await _context.Pricings.Where(x => x.Name == "Weekly").Select(y => y.PricingID).FirstOrDefaultAsync();
            var value = await _context.CarPricings.Where(w => w.PricingID == id).AverageAsync(x => x.Amount);
            return value;
        }
        public async Task<int> GetBlogCountAsync()
        {
            var value = await _context.Blogs.CountAsync();
            return value;
        }
        public Task<string> GetBlogTitleByMaxBlogCommentAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<int> GetBrandCountAsync()
        {
            var value = await _context.Brands.CountAsync();
            return value;
        }
        public Task<string> GetBrandNameByMaxCarAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetCarBrandAndModelByRentPriceDailyMaxAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetCarBrandAndModelByRentPriceDailyMinAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<int> GetCarCountAsync()
        {
            var value = await _context.Cars.CountAsync();
            return value;
        }
        public async Task<int> GetCarCountByFuelElectricAsync()
        {
            var value = await _context.Cars.Where(x => x.Fuel == "Electricity" || x.Fuel == "Diesel").CountAsync();
            return value;
        }
        public async Task<int> GetCarCountByFuelGasolineOrDieselAsync()
        {
            var value = await _context.Cars.Where(x => x.Fuel == "Gasoline" || x.Fuel == "Diesel").CountAsync();
            return value;
        }
        public async Task<int> GetCarCountByKmSmallerThen1000Async()
        {
            var value = await _context.Cars.Where(x => x.Mileage < 1000).CountAsync();
            return value;
        }
        public async Task<int> GetCarCountByTranmissionIsAutoAsync()
        {
            var value = await _context.Cars.Where(x => x.Transmission == "Automatic").CountAsync();
            return value;
        }
        public async Task<int> GetLocationCountAsync()
        {
            var value = await _context.Locations.CountAsync();
            return value;
        }
    }
}
