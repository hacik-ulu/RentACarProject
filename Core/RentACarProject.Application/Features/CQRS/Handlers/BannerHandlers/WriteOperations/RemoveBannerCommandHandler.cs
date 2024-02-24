using RentACarProject.Application.Features.CQRS.Commands.BannerCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.CQRS.Handlers.BannerHandlers.WriteOperations;

public class RemoveBannerCommandHandler
{
    private readonly IRepository<Banner> _repository;

    public RemoveBannerCommandHandler(IRepository<Banner> repository)
    {
        _repository = repository;
    }

    public async Task Handle(RemoveBannerCommand command)
    {
        var value = await _repository.GetByIdAsync(command.Id);
        await _repository.RemoveAsync(value);
    }
}
