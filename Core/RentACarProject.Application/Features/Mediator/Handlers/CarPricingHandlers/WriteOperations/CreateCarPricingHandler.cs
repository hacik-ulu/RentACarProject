using MediatR;
using RentACarProject.Application.Features.Mediator.Commands.CarPricingCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.CarPricingHandlers.WriteOperations
{
    public class CreateCarPricingHandler : IRequestHandler<CreateCarPricingCommand>
    {
        private readonly IRepository<CarPricing> _repository;

        public CreateCarPricingHandler(IRepository<CarPricing> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateCarPricingCommand request, CancellationToken cancellationToken)
        {
            // PricingIDs ve Amounts listelerinin aynı uzunlukta olduğunu varsayıyoruz
            for (int i = 0; i < request.PricingIDs.Count; i++)
            {
                var carPricing = new CarPricing
                {
                    CarID = request.CarID,
                    PricingID = request.PricingIDs[i], // Her bir PricingID'yi alıyoruz
                    Amount = request.Amounts[i] // Her bir bedeli ayrı ayrı ekliyoruz
                };

                await _repository.CreateAsync(carPricing); // Veritabanına ekliyoruz
            }

            // Burada herhangi bir dönüş değeri yok
        }
    }
}
