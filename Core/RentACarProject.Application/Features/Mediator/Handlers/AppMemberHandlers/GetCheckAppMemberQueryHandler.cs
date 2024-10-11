using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.AppMembersQueries;
using RentACarProject.Application.Features.Mediator.Results.AppUserMemberResults;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.AppMemberHandlers
{
    public class GetCheckAppMemberQueryHandler : IRequestHandler<GetCheckAppMemberQuery, GetCheckAppMemberQueryResult>
    {
        private readonly IRepository<AppUser> _appUserRepository;
        private readonly IRepository<AppRole> _appRoleRepository;

        public GetCheckAppMemberQueryHandler(IRepository<AppUser> appUserRepository, IRepository<AppRole> appRoleRepository)
        {
            _appUserRepository = appUserRepository;
            _appRoleRepository = appRoleRepository;
        }

        public async Task<GetCheckAppMemberQueryResult> Handle(GetCheckAppMemberQuery request, CancellationToken cancellationToken)
        {
            var values = new GetCheckAppMemberQueryResult();
            var user = await _appUserRepository.GetByFilterAsync(x => x.Email == request.Email && x.Password == request.Password);


            if (user == null)
            {
                values.IsExist = false;
            }
            else
            {
                values.IsExist = true;
                values.Email = user.Email;
                values.Role = (await _appRoleRepository.GetByFilterAsync(x => x.AppRoleID == user.AppRoleID)).AppRoleName;
                values.ID = user.AppUserID;
            }

            return values;
        }
    }
}
