using MediatR;
using RentACarProject.Application.Enums;
using RentACarProject.Application.Features.Mediator.Commands.AppMemberCommands;
using RentACarProject.Application.Features.Mediator.Commands.RegisterCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.AppMemberHandlers
{
    public class CreateAppMemberCommandHandler : IRequestHandler<CreateAppMemberCommand>
    {
        private readonly IRepository<AppUser> _repository;
        public CreateAppMemberCommandHandler(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateAppMemberCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new AppUser
            {
                Password = request.Password,
                Username = request.Username,
                AppRoleID = (int)RolesType.Member,
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname
            });
        }
    }
}
