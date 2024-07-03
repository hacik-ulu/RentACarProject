using MediatR;
using RentACarProject.Application.Features.Mediator.Commands.AppUserCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class UpdateAdminEmailCommandHandler : IRequestHandler<UpdateAdminEmailCommand>
    {
        private readonly IRepository<AppUser> _appUserRepository;

        public UpdateAdminEmailCommandHandler(IRepository<AppUser> appUserRepository)
        {
            _appUserRepository = appUserRepository ?? throw new ArgumentNullException(nameof(appUserRepository));
        }

        public async Task Handle(UpdateAdminEmailCommand request, CancellationToken cancellationToken)
        {
            var admin = await _appUserRepository.GetByIdAsync(request.AppUserID);

            if (admin == null)
            {
                throw new ApplicationException("Admin not found.");
            }

            admin.Email = request.NewEmail;
            await _appUserRepository.UpdateAsync(admin);
        }
    }
}

