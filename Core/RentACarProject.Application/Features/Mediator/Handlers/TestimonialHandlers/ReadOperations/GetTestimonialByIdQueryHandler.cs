using MediatR;
using RentACarProject.Application.Features.Mediator.Queries.TestimonialsQueries;
using RentACarProject.Application.Features.Mediator.Results.TestimonialResults;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.TestimonialHandlers.ReadOperations
{
    public class GetTestimonialByIdQueryHandle : IRequestHandler<GetTestimonialByIdQuery, GetTestimonialByIdQueryResult>
    {
        private readonly IRepository<Testimonial> _repository;

        public GetTestimonialByIdQueryHandle(IRepository<Testimonial> repository)
        {
            _repository = repository;
        }
        public async Task<GetTestimonialByIdQueryResult> Handle(GetTestimonialByIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.Id);
            return new GetTestimonialByIdQueryResult
            {
                Comment = values.Comment,
                ImageUrl = values.ImageUrl,
                Name = values.Name,
                TestimonialID = values.TestimonialID,
                Title = values.Title
            };
        }
    }
}
