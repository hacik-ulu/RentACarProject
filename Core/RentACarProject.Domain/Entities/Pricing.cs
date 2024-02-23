namespace RentACarProject.Domain.Entities;

public class Pricing
{
    public int PricingID { get; set; }
    public string Name { get; set; }  // PaymentType
    public List<CarPricing> CarPricings { get; set; }
}
