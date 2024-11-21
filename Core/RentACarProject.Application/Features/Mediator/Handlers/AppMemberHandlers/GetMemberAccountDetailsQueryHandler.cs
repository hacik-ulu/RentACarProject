using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using RentACarProject.Application.Features.Mediator.Queries.AppMembersQueries;
using RentACarProject.Application.Features.Mediator.Queries.AppUserQueries;
using RentACarProject.Application.Features.Mediator.Results.AppUserMemberResults;
using RentACarProject.Application.Features.Mediator.Results.AppUserResults;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.AppMemberHandlers
{
    public class GetMemberAccountDetailsQueryHandler : IRequestHandler<GetMemberAccountDetailsByIdQuery, MemberAccountDetailsQueryResult>
    {
        private readonly IRepository<AppUser> _appUserRepository;
        public GetMemberAccountDetailsQueryHandler(IRepository<AppUser> appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }
        public async Task<MemberAccountDetailsQueryResult> Handle(GetMemberAccountDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var member = await _appUserRepository.GetByIdAsync(request.AppUserID);
            if (member == null)
            {
                throw new InvalidOperationException($"Member with ID {request.AppUserID} was not found.");

            }

            return new MemberAccountDetailsQueryResult
            {
                AppUserID = member.AppUserID,
                Name = member.Name,
                Surname = member.Surname,
                Username = member.Username,
                Password = member.Password,
                Email = member.Email,
            };
        }
    }
}
