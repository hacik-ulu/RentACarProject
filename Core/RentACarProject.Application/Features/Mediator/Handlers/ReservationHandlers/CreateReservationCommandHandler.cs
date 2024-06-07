using MediatR;
using NETCore.MailKit.Core;
using RentACarProject.Application.Features.Mediator.Commands.ReservationCommands;
using RentACarProject.Application.Interfaces.EmailInterfaces;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using Microsoft.Extensions.Logging; // Loglama için gerekli

namespace RentACarProject.Application.Features.Mediator.Handlers.ReservationHandlers
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand>
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IEmailRepository _emailRepository;
        private readonly ILogger<CreateReservationCommandHandler> _logger; // Loglama için

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

                await _emailRepository.SendEmailAsync(request.Email, "Rezervasyon Onayı", "Rezervasyonunuz başarıyla tamamlandı!");

                _logger.LogInformation("Confirmation email sent successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while creating reservation or sending email: {ex.ToString()}");
                throw; // Hatanın yukarıya fırlatılması
            }

        }
    }
}
