using MediatR;
using RentACarProject.Application.Features.Mediator.Results.AppUserMemberResults;
using RentACarProject.Application.Features.Mediator.Results.AppUserResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Queries.AppMembersQueries
{
    public class GetMemberAccountDetailsByIdQuery : IRequest<MemberAccountDetailsQueryResult>
    {
        public int AppUserID { get; set; }

    }
}
