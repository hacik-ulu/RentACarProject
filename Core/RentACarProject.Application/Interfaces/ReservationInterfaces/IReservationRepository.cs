using RentACarProject.Application.Features.Mediator.Results.ReservatioResults;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Interfaces.ReservationInterfaces
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetReservationsAsync();

    }
}
