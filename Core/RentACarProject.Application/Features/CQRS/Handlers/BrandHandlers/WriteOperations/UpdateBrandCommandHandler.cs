using RentACarProject.Application.Features.CQRS.Commands.BrandCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.CQRS.Handlers.BrandHandlers.WriteOperations;

public class UpdateBrandCommandHandler
{
    private readonly IRepository<Brand> _repository;

    public UpdateBrandCommandHandler(IRepository<Brand> repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateBrandCommand command)
    {
        var values = await _repository.GetByIdAsync(command.BrandID);
        values.Name = command.Name;
        await _repository.UpdateAsync(values);
    }
}
