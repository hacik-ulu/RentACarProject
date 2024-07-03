using MediatR;
using RentACarProject.Application.Features.Mediator.Results.AppUserResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Queries.AppUserQueries
{
    public class GetAdminAccountDetailsByIDQuery : IRequest<AdminAccountDetailsQueryResult>
    {
        public int AppUserID { get; set; }
    }
}
