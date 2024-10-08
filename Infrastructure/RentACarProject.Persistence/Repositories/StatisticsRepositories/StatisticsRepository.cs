﻿using Microsoft.EntityFrameworkCore;
using RentACarProject.Application.Interfaces.StatisticsInterfaces;
using RentACarProject.Domain.Entities;
using RentACarProject.Persistence.Context;
using System.Text.RegularExpressions;

namespace RentACarProject.Persistence.Repositories.StatisticsRepositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly RentACarContext _context;

        public StatisticsRepository(RentACarContext context)
        {
            _context = context;
        }

        public async Task<int> GetAuthorCountAsync()
        {
            var value = await _context.Authors.CountAsync();
            return value;
        }
        public async Task<decimal> GetAvgRentPriceForMonthlyAsync()
        {
            int id = await _context.Pricings.Where(x => x.Name == "Monthly").Select(y => y.PricingID).FirstOrDefaultAsync();
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
        public async Task<string> GetBlogTitleByMaxBlogCommentAsync()
        {
            var result = (from blog in _context.Blogs
                          join comment in _context.Comments on blog.BlogID equals comment.BlogID into commentGroup
                          orderby commentGroup.Count() descending
                          select blog.Title).FirstOrDefault();

            return result;
        }
        public async Task<int> GetBrandCountAsync()
        {
            var value = await _context.Brands.CountAsync();
            return value;
        }
        public async Task<string> GetBrandNameByMaxCarAsync()
        {
            var result = await _context.Cars
                .GroupBy(car => car.Brand.Name)
                .Select(group => new
                {
                    BrandName = group.Key,
                    TotalCar = group.Count()
                })
                .OrderByDescending(item => item.TotalCar)
                .Take(1)
                .FirstOrDefaultAsync();

            return result.BrandName;
        }
        public async Task<string> GetCarBrandAndModelByRentPriceDailyMaxAsync()
        {
            // Select * From CarPricings where Amount=(Select Max(Amount) From CarPricings where PricingID=3)
            int pricingID = await _context.Pricings.Where(x => x.Name == "Per Day").Select(y => y.PricingID).FirstOrDefaultAsync();
            decimal amount = await _context.CarPricings.Where(y => y.PricingID == pricingID).MaxAsync(x => x.Amount);
            int carID = await _context.CarPricings.Where(x => x.Amount == amount).Select(y => y.CarID).FirstOrDefaultAsync();
            string brandModel = await _context.Cars.Where(x => x.CarID == carID).Include(y => y.Brand).Select(z => z.Brand.Name + " " + z.Model).FirstOrDefaultAsync();
            return brandModel;
        }
        public async Task<string> GetCarBrandAndModelByRentPriceDailyMinAsync()
        {
            // Select * From CarPricings where Amount=(Select Max(Amount) From CarPricings where PricingID=3)
            int pricingID = await _context.Pricings.Where(x => x.Name == "Per Day").Select(y => y.PricingID).FirstOrDefaultAsync();
            decimal amount = await _context.CarPricings.Where(y => y.PricingID == pricingID).MinAsync(x => x.Amount);
            int carID = await _context.CarPricings.Where(x => x.Amount == amount).Select(y => y.CarID).FirstOrDefaultAsync();
            string brandModel = await _context.Cars.Where(x => x.CarID == carID).Include(y => y.Brand).Select(z => z.Brand.Name + " " + z.Model).FirstOrDefaultAsync();
            return brandModel;
        }
        public async Task<int> GetCarCountAsync()
        {
            var value = await _context.Cars.CountAsync();
            return value;
        }
        public async Task<int> GetCarCountByFuelElectricAsync()
        {
            var value = await _context.Cars.Where(x => x.Fuel == "Electricity").CountAsync();
            return value;
        }
        public async Task<int> GetCarCountByFuelGasolineOrDieselAsync()
        {
            var value = await _context.Cars.Where(x => x.Fuel == "Gasoline" || x.Fuel == "Diesel").CountAsync();
            return value;
        }
        public async Task<int> GetCarCountByKmSmallerThen10000Async()
        {
            var value = await _context.Cars.Where(x => x.Mileage < 10000).CountAsync();
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
