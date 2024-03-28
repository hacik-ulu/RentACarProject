using MediatR;
using RentACarProject.Application.Features.Mediator.Commands.TagCloudsCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.Mediator.Handlers.TagCloudHandlers.WriteOperations
{
    public class UpdateTagCloudCommandHandler : IRequestHandler<UpdateTagCloudCommand>
    {
        private readonly IRepository<TagCloud> _repository;

        public UpdateTagCloudCommandHandler(IRepository<TagCloud> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateTagCloudCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.TagCloudID);
            value.Name = request.Name;
            value.BlogID = request.BlogID;
            await _repository.UpdateAsync(value);
        }
    }
}
