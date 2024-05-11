namespace RentACarProject.Domain.Entities;

public class Location
{
    public int LocationID { get; set; }
    public string Name { get; set; }
    public List<RentCar> RentCars { get; set; }
}
