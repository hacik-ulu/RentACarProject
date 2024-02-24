using RentACarProject.Application.Features.CQRS.Commands.CarCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.CQRS.Handlers.CarHandlers.WriteOperations;

public class UpdateCarCommandHandler
{
    private readonly IRepository<Car> _repository;

    public UpdateCarCommandHandler(IRepository<Car> repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateCarCommand command)
    {
        var values = await _repository.GetByIdAsync(command.CarID);
        values.Fuel = command.Fuel;
        values.Transmission = command.Transmission;
        values.BigImageUrl = command.BigImageUrl;
        values.BrandID = command.BrandID;
        values.CoverImagerUrl = command.CoverImagerUrl;
        values.Mileage = command.Mileage;
        values.Luggage = command.Luggage;
        values.Model = command.Model;
        values.Seat = command.Seat;
        await _repository.UpdateAsync(values);
    }
}
