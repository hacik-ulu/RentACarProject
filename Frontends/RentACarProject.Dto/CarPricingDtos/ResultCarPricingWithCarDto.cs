﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Dto.CarPricingDtos
{
    public class ResultCarPricingWithCarDto
    {
        public int CarID { get; set; }
        public int CarPricingId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Amount { get; set; }
        public string CoverImageUrl { get; set; }
    }
}
