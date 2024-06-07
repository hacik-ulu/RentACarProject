using MediatR;
using NETCore.MailKit.Core;
using RentACarProject.Application.Features.Mediator.Commands.ReservationCommands;
using RentACarProject.Application.Interfaces.EmailInterfaces;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.ConstrainedExecution;
using RentACarProject.Application.Interfaces.CarInterfaces;

namespace RentACarProject.Application.Features.Mediator.Handlers.ReservationHandlers
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand>
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IEmailRepository _emailRepository;
        private readonly ILogger<CreateReservationCommandHandler> _logger; 

        public CreateReservationCommandHandler(IRepository<Reservation> repository, IEmailRepository emailRepository, ILogger<CreateReservationCommandHandler> logger)
        {
            _repository = repository;
            _emailRepository = emailRepository;
            _logger = logger;
        }

        public async Task Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating reservation...");
                var reservation = new Reservation
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

                await _repository.CreateAsync(reservation);

                _logger.LogInformation("Reservation created successfully. Sending confirmation email...");

                string sendMail = GenerateReservationConfirmationEmail(request);

                await _emailRepository.SendEmailAsync(request.Email, "Reservation Accepted", sendMail);

                _logger.LogInformation("Confirmation email sent successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while creating reservation or sending email: {ex.ToString()}");
                throw;
            }

        }

        public static string GenerateReservationConfirmationEmail(CreateReservationCommand sendMail)
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
                            <td>Name</td>
                            <td>" + sendMail.Name + @"</td>
                        </tr>
                        <tr>
                            <td>Surname</td>
                            <td>" + sendMail.Surname + @"</td>
                        </tr>
                        <tr>
                            <td>Your Requests</td>
                            <td>" + sendMail.Description + @"</td>
                        </tr>
                        <tr>
                            <td>Age</td>
                            <td>" + sendMail.Age + @"</td>
                        </tr>
                        <tr>
                            <td>Driver's License Year</td>
                            <td>" + sendMail.DriverLicenseYear + @"</td>
                        </tr>
                        <tr>
                            <td>Phone</td>
                            <td>" + sendMail.Phone + @"</td>
                        </tr>
                        <tr>
                            <td>Email Adress</td>
                            <td>" + sendMail.Email + @"</td>
                        </tr>
                        <tr>
                            <td>Pick Up Location ID</td>
                            <td>" + sendMail.PickUpLocationID + @"</td>
                        </tr>
                        <tr>
                            <td>Drop Off Location ID</td>
                            <td>" + sendMail.DropOffLocationID + @"</td>
                        </tr>
                    </table>
                </div>
            </body>
            </html>";

            return body;
        }
    }
}
