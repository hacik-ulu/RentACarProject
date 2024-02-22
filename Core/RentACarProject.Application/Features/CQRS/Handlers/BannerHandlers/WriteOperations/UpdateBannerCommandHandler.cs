using RentACarProject.Application.Features.CQRS.Commands.BannerCommands;
using RentACarProject.Application.Interfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.CQRS.Handlers.BannerHandlers.WriteOperations;

public class UpdateBannerCommandHandler
{
    private readonly IRepository<Banner> _repository;

    public UpdateBannerCommandHandler(IRepository<Banner> repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateBannerCommand command)
    {
        var values = await _repository.GetByIdAsync(command.BannerID);
        values.Description = command.Description;
        values.Title = command.Title;
        values.VideoUrl = command.VideoUrl;
        values.VideoDescription = command.VideoDescription;
        await _repository.UpdateAsync(values);

    }
}
