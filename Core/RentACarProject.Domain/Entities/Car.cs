﻿namespace RentACarProject.Domain.Entities;

public class Car
{
    public int CarID { get; set; }
    public int BrandID { get; set; }
    public Brand Brand { get; set; }
    public string Model { get; set; }
    public string CoverImagerUrl { get; set; }
    public decimal Mileage { get; set; }
    public int Year { get; set; }
    public string Transmission { get; set; }
    public byte Seat { get; set; }
    public byte Luggage { get; set; }
    public string Fuel { get; set; }
    public string BigImageUrl { get; set; }
    public List<CarFeature> CarFeatures { get; set; }
    public List<CarDescription> CarDescriptions { get; set; }
    public List<CarPricing> CarPricings { get; set; }
    public List<RentCar> RentCars { get; set; }
    public List<RentCarProcess> RentCarProcesses { get; set; }
    public List<Reservation> Reservations { get; set; }
    public List<Review> Reviews { get; set; }

}

