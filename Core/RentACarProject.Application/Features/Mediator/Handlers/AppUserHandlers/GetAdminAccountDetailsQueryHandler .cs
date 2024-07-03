using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.AppUserQueries;
using RentACarProject.Application.Features.Mediator.Results.AppUserResults;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class GetAdminAccountDetailsQueryHandler : IRequestHandler<GetAdminAccountDetailsByIDQuery, AdminAccountDetailsQueryResult>
    {
        private readonly IRepository<AppUser> _appUserRepository;

        public GetAdminAccountDetailsQueryHandler(IRepository<AppUser> appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<AdminAccountDetailsQueryResult> Handle(GetAdminAccountDetailsByIDQuery request, CancellationToken cancellationToken)
        {
            var admin = await _appUserRepository.GetByIdAsync(request.AppUserID);
            if (admin == null)
            {
                Console.WriteLine("Error!");

            }

            return new AdminAccountDetailsQueryResult
            {
                AppUserID = admin.AppUserID,
                Username = admin.Username,
                Password = admin.Password,
                Email = admin.Email,
            };
        }
    }
}
