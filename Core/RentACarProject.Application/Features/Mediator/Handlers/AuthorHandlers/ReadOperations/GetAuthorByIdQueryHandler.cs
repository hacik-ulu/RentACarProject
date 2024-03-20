using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.AuthorQueries;
using RentACarProject.Application.Features.Mediator.Results.FooterAddressResults;
using RentACarProject.Application.Features.Mediator.Results.AuthorResults;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.AuthorHandlers.ReadOperations
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, GetAuthorByIdQueryResult>
    {
        private readonly IRepository<Author> _repository;

        public GetAuthorByIdQueryHandler(IRepository<Author> repository)
        {
            _repository = repository;
        }
        public async Task<GetAuthorByIdQueryResult> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.Id);
            return new GetAuthorByIdQueryResult
            {
                AuthorID = values.AuthorID,
                Name = values.Name,
                ImageUrl = values.ImageUrl,
                Description = values.Description
            };
        }
    }
}
