using MediatR;
using RentACarProject.Application.Features.Mediator.Commands.TestimonialCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Testimonials.Mediator.Handlers.TestimonialHandlers.WriteOperations
{
    public class UpdateTestimonialCommandHandler : IRequestHandler<UpdateTestimonialCommand>
    {
        private readonly IRepository<Testimonial> _repository;

        public UpdateTestimonialCommandHandler(IRepository<Testimonial> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateTestimonialCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.TestimonialID);
            value.Name = request.Name;
            value.Comment = request.Comment;
            value.Title = request.Title;
            value.ImageUrl = request.ImageUrl;
            await _repository.UpdateAsync(value);
        }
    }

}

