using MediatR;
using RentACarProject.Application.Features.Mediator.Results.AuthorResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Queries.AuthorQueries
{
    public class GetBlogsByAuthorIdQuery : IRequest<List<GetBlogsByAuthorIdQueryResult>>
    {
        public int AuthorID { get; set; }

        public GetBlogsByAuthorIdQuery(int authorId)
        {
            AuthorID = authorId;
        }
    }
}
