using MediatR;
using RentACarProject.Application.Features.Mediator.Commands.AppUserCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly IRepository<AppUser> _appUserRepository;

        public ChangePasswordCommandHandler(IRepository<AppUser> appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _appUserRepository.GetByFilterAsync(x => x.Username == request.Username);
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
