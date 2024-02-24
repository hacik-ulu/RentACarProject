using RentACarProject.Application.Features.CQRS.Commands.AboutCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.CQRS.Handlers.AboutHandlers.WriteOperations;

public class UpdateAboutCommandHandler
{
    private readonly IRepository<About> _repository;

    public UpdateAboutCommandHandler(IRepository<About> repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateAboutCommand command)
    {
        var values = await _repository.GetByIdAsync(command.AboutId);
        values.Description = command.Description;
        values.Title = command.Title;
        values.ImageUrl = command.ImageUrl;
        await _repository.UpdateAsync(values);
    }
}
