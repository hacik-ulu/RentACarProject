using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Domain.Entities;

// 1 numaralı aracığın 1 numaralı(ödeme planı) süreçteki kiralama bedeli.
public class CarPricing
{
    public int CarPricingID { get; set; }
    public int CarID { get; set; }
    public Car Car { get; set; }
    public int PricingID { get; set; }
    public Pricing Pricing { get; set; }
    public decimal Amount { get; set; }

}