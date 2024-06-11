using Microsoft.EntityFrameworkCore;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Application.Interfaces.ReservationInterfaces;
using RentACarProject.Domain.Entities;
using RentACarProject.Persistence.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACarProject.Persistence.Repositories.GeneralRepository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly RentACarContext _context;

        public ReservationRepository(RentACarContext context)
        {
            _context = context;
        }

        public async Task<List<Reservation>> GetReservationsAsync()
        {
            var reservations = await _context.Reservations
                .Include(r => r.PickUpLocation)
                .Include(r => r.DropOffLocation)
                .Include(r => r.Car)
                    .ThenInclude(c => c.Brand)
                .ToListAsync();

            return reservations;
        }
    }
}
