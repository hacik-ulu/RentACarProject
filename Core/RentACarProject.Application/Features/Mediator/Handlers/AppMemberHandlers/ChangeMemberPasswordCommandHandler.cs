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
    public class ChangeMemberPasswordCommandHandler : IRequestHandler<ChangeMemberPasswordCommand>
    {
        private readonly IRepository<AppUser> _appUserRepository;
        public ChangeMemberPasswordCommandHandler(IRepository<AppUser> appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }
        public async Task Handle(ChangeMemberPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _appUserRepository.GetByFilterAsync(x => x.Email == request.Email);
                if (user == null)
                {
                    throw new KeyNotFoundException("User not found.");
                }

                user.Password = request.NewPassword;

                await _appUserRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred: {ex.Message}");
            }
        }
    }
}
