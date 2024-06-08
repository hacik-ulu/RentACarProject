using MediatR;
using NETCore.MailKit.Core;
using RentACarProject.Application.Features.Mediator.Commands.ReservationCommands;
using RentACarProject.Application.Interfaces.EmailInterfaces;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Application.Interfaces.CarInterfaces;
using RentACarProject.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.ReservationHandlers
{
    public class CreateReservationHandler : IRequestHandler<CreateReservationCommand>
    {
        private readonly IRepository<Reservation> _reservationRepository;
        private readonly IRepository<Location> _locationRepository;
        private readonly IRepository<Car> _carRepository;
        private readonly IRepository<Brand> _brandRepository;
        private readonly IEmailRepository _emailRepository;

        private readonly ILogger<CreateReservationHandler> _logger;

        public CreateReservationHandler(IRepository<Reservation> reservationRepository, IRepository<Car> carRepository, IRepository<Brand> brandRepository, ILogger<CreateReservationHandler> logger, IEmailRepository emailRepository, IRepository<Location> locationRepository)
        {
            _reservationRepository = reservationRepository;
            _locationRepository = locationRepository;
            _carRepository = carRepository;
            _brandRepository = brandRepository;
            _logger = logger;
            _emailRepository = emailRepository;
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

                // Retrieve the created reservation from the database
                var createdReservation = await _reservationRepository.GetByIdAsync(newReservation.ReservationID);

                // Retrieve the car details from the Cars table
                var car = await _carRepository.GetByIdAsync(request.CarID);

                // Retrieve the brand details from the Brands table using the BrandID from the car
                var brand = await _brandRepository.GetByIdAsync(car.BrandID);

                // Retrieve the pickup location details
                var pickupLocation = await _locationRepository.GetByIdAsync(request.PickUpLocationID);

                // Retrieve the dropoff location details
                var dropOffLocation = await _locationRepository.GetByIdAsync(request.DropOffLocationID);


                _logger.LogInformation("Reservation created successfully. Sending confirmation email...");

                string confirmationEmailBody = GenerateReservationConfirmationEmail(createdReservation, car, brand, pickupLocation, dropOffLocation);

                await _emailRepository.SendEmailAsync(request.Email, "Reservation Accepted", confirmationEmailBody);

                _logger.LogInformation("Confirmation email sent successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while creating reservation or sending email: {ex}");
                throw;
            }
        }

        public static string GenerateReservationConfirmationEmail(Reservation reservation, Car car, Brand brand, Location pickupLocation, Location dropOffLocation)
        {
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
                                <td>Age</td>
                                <td>" + reservation.Age + @"</td>
                            </tr>
                            <tr>
                                <td>Driver's License Year</td>
                                <td>" + reservation.DriverLicenseYear + @"</td>
                            </tr>
                            <tr>
                                <td>Email Adress</td>
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
                        </table>
                        Rapid Rent Team</p>
                        <p>You can contact us for any questions or details you may have. Have fun driving.</p><br>
                        <p>Best Regards</p>
                    </div>
                </body>
                </html>";

            return body;
        }
    }
}

//price değerine de bakalım.