using MediatR;
using RentACarProject.Application.Features.Mediator.Commands.TestimonialCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Testimonials.Mediator.Handlers.TestimonialHandlers.WriteOperations
{
    public class RemoveTestimonialCommandHandler : IRequestHandler<RemoveTestimonialCommand>
    {
        private readonly IRepository<Testimonial> _repository;

        public RemoveTestimonialCommandHandler(IRepository<Testimonial> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveTestimonialCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.Id);
            await _repository.RemoveAsync(value);
        }

    }
}
