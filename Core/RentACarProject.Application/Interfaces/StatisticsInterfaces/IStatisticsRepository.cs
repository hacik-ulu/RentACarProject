using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Interfaces.StatisticsInterfaces
{
    public interface IStatisticsRepository
    {
        Task<int> GetCarCountAsync();
        Task<int> GetLocationCountAsync();
        Task<int> GetAuthorCountAsync();
        Task<int> GetBlogCountAsync();
        Task<int> GetBrandCountAsync();
        Task<decimal> GetAvgRentPriceForMonthlyAsync();
        Task<decimal> GetAvgRentPriceForPerDayAsync();
        Task<decimal> GetAvgRentPriceForWeeklyAsync();
        Task<int> GetCarCountByTranmissionIsAutoAsync();
        Task<string> GetBrandNameByMaxCarAsync();
        Task<string> GetBlogTitleByMaxBlogCommentAsync();
        Task<int> GetCarCountByKmSmallerThen10000Async();
        Task<int> GetCarCountByFuelGasolineOrDieselAsync();
        Task<int> GetCarCountByFuelElectricAsync();
        Task<string> GetCarBrandAndModelByRentPriceDailyMaxAsync();
        Task<string> GetCarBrandAndModelByRentPriceDailyMinAsync();
    }
}
