using MediatR;
using RentACarProject.Application.Features.Mediator.Results.TagCloudResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Queries.TagCloudQueries
{
    public class GetTagCloudQuery :IRequest<List<GetTagCloudQueryResult>>
    {
    }
}
