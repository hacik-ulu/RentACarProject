using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.CQRS.Results.CarResults;

public class GetCarQueryResult
{
    public int CarID { get; set; }
    public int BrandID { get; set; }
    public string Model { get; set; }
    public string CoverImagerUrl { get; set; }
    public decimal Mileage { get; set; }
    public string Transmission { get; set; }
    public byte Seat { get; set; }
    public byte Luggage { get; set; }
    public string Fuel { get; set; }
    public string BigImageUrl { get; set; }
   
}
