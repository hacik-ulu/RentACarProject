using MediatR;
using RentACarProject.Application.Features.Mediator.Commands.AppUserCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class UpdateAdminUsernameCommandHandler : IRequestHandler<UpdateAdminUsernameCommand>
    {
        private readonly IRepository<AppUser> _appUserRepository;
        public UpdateAdminUsernameCommandHandler(IRepository<AppUser> appUserRepository)
        {
            _appUserRepository = appUserRepository ?? throw new ArgumentNullException(nameof(appUserRepository));
        }

        public async Task Handle(UpdateAdminUsernameCommand request, CancellationToken cancellationToken)
        {
            var admin = await _appUserRepository.GetByIdAsync(request.AppUserID);

            if (admin == null)
            {
                throw new ApplicationException("Admin not found.");
            }

            admin.Username = request.NewUsername;
            await _appUserRepository.UpdateAsync(admin);
        }
    }
}
