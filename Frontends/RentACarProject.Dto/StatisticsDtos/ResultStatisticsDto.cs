﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.StatisticsDtos
{
    public class ResultStatisticsDto
    {
        public int CarCount { get; set; }
        public int LocationCount { get; set; }
        public int AuthorCount { get; set; }
        public int BlogCount { get; set; }
        public int BrandCount { get; set; }
        public decimal AvgPriceForDaily { get; set; }
        public decimal AvgRentPriceForWeekly { get; set; }
        public decimal AvgRentPriceForMonthly { get; set; }
        public int CarCountByTransmissionIsAuto { get; set; }
        public int CarCountByKmSmallerThen10000 { get; set; }
        public int CarCountByFuelGasolineOrDiesel { get; set; }
        public int CarCountByFuelElectric { get; set; }
        public string CarBrandNameAndModelByRentPriceDailyMax { get; set; }
        public string CarBrandNameAndModelByRentPriceDailyMin { get; set; }
        public string BrandNameByMaxCar { get; set; }
        public string BlogTitleByMaxBlogComment { get; set; }
    }
}
