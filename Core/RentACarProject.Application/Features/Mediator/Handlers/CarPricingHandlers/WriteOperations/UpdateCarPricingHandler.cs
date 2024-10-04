using MediatR;
using RentACarProject.Application.Features.Mediator.Commands.CarPricingCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.CarPricingHandlers.WriteOperations
{
    public class UpdateCarPricingHandler : IRequestHandler<UpdateCarPricingCommand>
    {
        private readonly IRepository<CarPricing> _repository;

        public UpdateCarPricingHandler(IRepository<CarPricing> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateCarPricingCommand request, CancellationToken cancellationToken)
        {
            // Belirtilen CarID için mevcut Pricing kayıtlarını al
            var currentPricingList = await _repository.GetAllAsync(); // Tüm kayıtları alıyoruz
            var carPricingList = currentPricingList
                .Where(cp => cp.CarID == request.CarID) // Sadece belirli CarID'lere göre filtreleme
                .ToList();

            foreach (var pricingAmount in request.PricingAmounts)
            {
                var existingPricing = carPricingList.FirstOrDefault(cp => cp.PricingID == pricingAmount.PricingID); // Mevcut PricingID'yi bul

                if (existingPricing != null)
                {
                    // Kayıtları güncelle
                    existingPricing.Amount = pricingAmount.Amount; 

                    await _repository.UpdateAsync(existingPricing); 
                }
                else
                {
                    throw new KeyNotFoundException($"PricingID {pricingAmount.PricingID} not found for CarID {request.CarID}.");
                }
            }
        }
    }
}
