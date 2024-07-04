using MediatR;
using NETCore.MailKit.Core;
using RentACarProject.Application.Features.Mediator.Commands.ReservationCommands;
using RentACarProject.Application.Interfaces.EmailInterfaces;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Application.Interfaces.CarInterfaces;
using RentACarProject.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RentACarProject.Application.Features.Mediator.Handlers.ReservationHandlers.WriteOperations
{
    public class CreateReservationHandler : IRequestHandler<CreateReservationCommand>
    {
        private readonly IRepository<Reservation> _reservationRepository;
        private readonly IRepository<Location> _locationRepository;
        private readonly IRepository<Car> _carRepository;
        private readonly IRepository<Brand> _brandRepository;
        private readonly IRepository<CarPricing> _carPricingRepository;
        private readonly IEmailRepository _emailRepository;

        private readonly ILogger<CreateReservationHandler> _logger;

        public CreateReservationHandler(IRepository<Reservation> reservationRepository, IRepository<Car> carRepository, IRepository<Brand> brandRepository, ILogger<CreateReservationHandler> logger, IEmailRepository emailRepository, IRepository<Location> locationRepository, IRepository<CarPricing> carPricingRepository)
        {
            _reservationRepository = reservationRepository;
            _locationRepository = locationRepository;
            _carRepository = carRepository;
            _brandRepository = brandRepository;
            _logger = logger;
            _emailRepository = emailRepository;
            _carPricingRepository = carPricingRepository;
        }

        public async Task Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating reservation...");

                var newReservation = new Reservation
                {
                    Age = request.Age,
                    CarID = request.CarID,
                    Description = request.Description,
                    DriverLicenseYear = request.DriverLicenseYear,
                    DropOffLocationID = request.DropOffLocationID,
                    Email = request.Email,
                    Name = request.Name,
                    Phone = request.Phone,
                    PickUpLocationID = request.PickUpLocationID,
                    Surname = request.Surname,
                    Status = "Reservation Received."
                };

                await _reservationRepository.CreateAsync(newReservation);

                var createdReservation = await _reservationRepository.GetByIdAsync(newReservation.ReservationID);

                var car = await _carRepository.GetByIdAsync(request.CarID);

                var brand = await _brandRepository.GetByIdAsync(car.BrandID);

                var pickupLocation = await _locationRepository.GetByIdAsync(request.PickUpLocationID);

                var dropOffLocation = await _locationRepository.GetByIdAsync(request.DropOffLocationID);

                var pricingDetails = await _carPricingRepository.GetAllAsync();
                var carPricings = pricingDetails.Where(p => p.CarID == request.CarID && (p.PricingID == 1 || p.PricingID == 3 || p.PricingID == 4)).ToList();

                _logger.LogInformation("Reservation created successfully. Sending confirmation email...");

                string confirmationEmailBody = GenerateReservationConfirmationEmail(createdReservation, car, brand, pickupLocation, dropOffLocation, carPricings);

                await _emailRepository.SendEmailAsync(request.Email, "Reservation Accepted", confirmationEmailBody);

                _logger.LogInformation("Confirmation email sent successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while creating reservation or sending email: {ex}");
                throw;
            }
        }

        public static string GenerateReservationConfirmationEmail(Reservation reservation, Car car, Brand brand, Location pickupLocation, Location dropOffLocation, List<CarPricing> carPricings)
        {
            var pricingDetails = string.Join("<br>", carPricings.Select(p =>
            {
                string type = p.PricingID switch
                {
                    1 => "Daily",
                    3 => "Weekly",
                    4 => "Monthly",
                    _ => "Unknown"
                };
                return $"{type}: {p.Amount}₺";
            }));

            string body = @"
                <!DOCTYPE html>
                <html>
                <head>
                    <style>
                        body {
                            font-family: Arial, sans-serif;
                            line-height: 1.6;
                        }
                        .container {
                            max-width: 600px;
                            margin: 0 auto;
                            padding: 20px;
                            border: 1px solid #ccc;
                            border-radius: 5px;
                            background-color: #f9f9f9;
                        }
                        h1 {
                            color: #333;
                        }
                        p {
                            margin-bottom: 20px;
                        }
                        table {
                            width: 100%;
                            border-collapse: collapse;
                            margin-bottom: 20px;
                        }
                        th, td {
                            padding: 8px;
                            text-align: left;
                            border-bottom: 1px solid #ddd;
                        }
                        th {
                            background-color: #f2f2f2;
                        }
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <h1>Reservation Confirmation</h1>
                        <p>Your reservation has been successfully completed!</p>
                        <table>
                            <tr>
                                <th>Rental Details</th>
                            </tr>
                            <tr>
                                <td>Reservation Number</td>
                                <td>" + reservation.ReservationID + @"</td>
                            </tr>
                            <tr>
                                <td>Name</td>
                                <td>" + reservation.Name + @"</td>
                            </tr>
                            <tr>
                                <td>Surname</td>
                                <td>" + reservation.Surname + @"</td>
                            </tr>
                            <tr>
                                <td>Contact Number</td>
                                <td>" + reservation.Phone + @"</td>
                            </tr>
                            <tr>
                                <td>Email Address</td>
                                <td>" + reservation.Email + @"</td>
                            </tr>
                            <tr>
                                <td>Age</td>
                                <td>" + reservation.Age + @"</td>
                            </tr>
                            <tr>
                                <td>Driver's License Year</td>
                                <td>" + reservation.DriverLicenseYear + @"</td>
                            </tr>
                            <tr>
                                <td>Car Brand</td>
                                <td>" + brand.Name + @"</td>
                            </tr>
                            <tr>
                                <td>Car Model</td>
                                <td>" + car.Model + @"</td>
                            </tr>
                            <tr>
                                <td>Pick Up Location</td>
                                <td>" + pickupLocation.Name + @"</td>
                            </tr>
                            <tr>
                                <td>Drop Off Location</td>
                                <td>" + dropOffLocation.Name + @"</td>
                            </tr>
                            <tr>
                                <td>Pricing Details</td>
                                <td>" + pricingDetails + @"</td>
                            </tr>
                        </table>
                        <p>Rapid Rent Team</p>
                        <p>You can contact us for any questions or details you may have. Have fun driving.</p><br>
                        <p>Best Regards</p>
                    </div>
                </body>
                </html>";

            return body;
        }
    }
}
