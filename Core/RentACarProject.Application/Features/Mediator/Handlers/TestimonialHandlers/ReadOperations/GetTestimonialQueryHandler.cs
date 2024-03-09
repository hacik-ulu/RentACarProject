using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.TestimonailsQueries;
using RentACarProject.Application.Features.Mediator.Results.TestimonialResults;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

public class GetTestimonialQueryHandler : IRequestHandler<GetTestimonialQuery, List<GetTestimonialQueryResult>>
{
    private readonly IRepository<Testimonial> _repository;

    public GetTestimonialQueryHandler(IRepository<Testimonial> repository)
    {
        _repository = repository;
    }

    public async Task<List<GetTestimonialQueryResult>> Handle(GetTestimonialQuery request, CancellationToken cancellationToken)
    {
        var values = await _repository.GetAllAsync();
        return values.Select(x => new GetTestimonialQueryResult
        {
            Title = x.Title,
            TestimonialID = x.TestimonialID,
            Name = x.Name,
            ImageUrl = x.ImageUrl,
            Comment = x.Comment,
        }).ToList();
    }
}
