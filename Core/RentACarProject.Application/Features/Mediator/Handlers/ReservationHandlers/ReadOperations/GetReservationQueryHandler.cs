using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.ReservationQueries;
using RentACarProject.Application.Features.Mediator.Results.ReservatioResults;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Application.Interfaces.ReservationInterfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.ReservationHandlers.ReadOperations
{
    public class GetReservationQueryHandler : IRequestHandler<GetReservationQuery, List<GetReservationQueryResult>>
    {
        private readonly IReservationRepository _reservationRepository;

        public GetReservationQueryHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<List<GetReservationQueryResult>> Handle(GetReservationQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _reservationRepository.GetReservationsAsync();

            var results = new List<GetReservationQueryResult>();

            foreach (var reservation in reservations)
            {
                var result = new GetReservationQueryResult
                {
                    ReservationID = reservation.ReservationID,
                    Name = reservation.Name,
                    Surname = reservation.Surname,
                    Email = reservation.Email,
                    Phone = reservation.Phone,
                    PickUpLocationName = reservation.PickUpLocation.Name,
                    DropOffLocationName = reservation.DropOffLocation.Name,
                    CarID = reservation.Car.CarID,
                    CarModel = reservation.Car.Model,
                    BrandName = reservation.Car.Brand.Name,
                    Age = reservation.Age,
                    DriverLicenseYear = reservation.DriverLicenseYear,
                    Description = reservation.Description,
                    Status = reservation.Status
                };

                results.Add(result);
            }

            return results;
        }
    }
}
