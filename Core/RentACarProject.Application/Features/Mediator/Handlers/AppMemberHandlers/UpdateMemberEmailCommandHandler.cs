using MediatR;
using RentACarProject.Application.Features.Mediator.Commands.AppMemberCommands;
using RentACarProject.Application.Features.Mediator.Commands.AppUserCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.AppMemberHandlers
{
    public class UpdateMemberEmailCommandHandler : IRequestHandler<UpdateMemberEmailCommand>
    {
        private readonly IRepository<AppUser> _appUserRepository;
        public UpdateMemberEmailCommandHandler(IRepository<AppUser> appUserRepository)
        {
            _appUserRepository = appUserRepository ?? throw new ArgumentNullException(nameof(appUserRepository));
        }
        public async Task Handle(UpdateMemberEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _appUserRepository.GetByIdAsync(request.AppUserID);

            if (user == null)
            {
                throw new ApplicationException("user not found.");
            }

            user.Email = request.Email;
            await _appUserRepository.UpdateAsync(user);
        }
    }
}
