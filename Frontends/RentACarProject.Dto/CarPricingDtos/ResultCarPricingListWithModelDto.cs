﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.CarPricingDtos
{
	public class ResultCarPricingListWithModelDto
	{
        public int CarID { get; set; }
        public string Model { get; set; }
		public decimal DailyAmount { get; set; }
		public decimal WeeklyAmount { get; set; }
		public decimal MonthlyAmount { get; set; }
		public string CoverImagerUrl { get; set; }
		public string Brand { get; set; }
	}
}
