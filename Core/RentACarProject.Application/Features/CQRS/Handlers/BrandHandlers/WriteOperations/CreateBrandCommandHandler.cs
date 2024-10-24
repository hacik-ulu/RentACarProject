﻿using RentACarProject.Application.Features.CQRS.Commands.BrandCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

public class CreateBrandCommandHandler
{
    private readonly IRepository<Brand> _repository;

    public CreateBrandCommandHandler(IRepository<Brand> repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateBrandCommand command)
    {
        await _repository.CreateAsync(new Brand
        {
            Name = command.Name
        });
    }
}
