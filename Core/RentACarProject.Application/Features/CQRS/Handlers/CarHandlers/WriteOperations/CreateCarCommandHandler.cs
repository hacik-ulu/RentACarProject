using RentACarProject.Application.Features.CQRS.Commands.CarCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.CQRS.Handlers.CarHandlers.WriteOperations;

public class CreateCarCommandHandler
{
    private readonly IRepository<Car> _repository;

    public CreateCarCommandHandler(IRepository<Car> repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateCarCommand command)
    {
        await _repository.CreateAsync(new Car
        {
            BigImageUrl = command.BigImageUrl,
            Luggage = command.Luggage,
            Mileage = command.Mileage,
            Model = command.Model,
            Seat = command.Seat,
            Transmission = command.Transmission,
            CoverImagerUrl = command.CoverImagerUrl,
            BrandID = command.BrandID,
            Fuel = command.Fuel,
            Year = command.Year
        });
    }
}
