using System.Reflection.Metadata;

namespace RentACarProject.Domain.Entities;

public class Location
{
    public int LocationID { get; set; }
    public string Name { get; set; }
    public List<RentCar> RentCars { get; set; }
    public List<Reservation> PickUpReservation { get; set; }
    public List<Reservation> DropOffReservation { get; set; }
}
