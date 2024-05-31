using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.CarFeatureQueries;
using RentACarProject.Application.Features.Mediator.Queries.ReviewQueries;
using RentACarProject.Application.Features.Mediator.Results.CarFeaturesResults;
using RentACarProject.Application.Features.Mediator.Results.ReviewResults;
using RentACarProject.Application.Interfaces.CarFeatureInterfaces;
using RentACarProject.Application.Interfaces.ReviewInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.ReviewHandler.ReadOperations
{
    public class GetReviewByCarIDQueryHandler : IRequestHandler<GetReviewByCarIDQuery, List<GetReviewByCarIDQueryResult>>
    {
        private readonly IReviewRepository _repository;

        public GetReviewByCarIDQueryHandler(IReviewRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetReviewByCarIDQueryResult>> Handle(GetReviewByCarIDQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetReviewsByCarIDAsync(request.Id);
            return values.Select(x => new GetReviewByCarIDQueryResult
            {
               CarID = x.CarID,
               Comment = x.Comment,
               CustomerImage = x.CustomerImage,
               CustomerName = x.CustomerName,
               RatingValue = x.RatingValue,
               ReviewDate = x.ReviewDate,
               ReviewID = x.ReviewID
            }).ToList();

        }
    }
}
