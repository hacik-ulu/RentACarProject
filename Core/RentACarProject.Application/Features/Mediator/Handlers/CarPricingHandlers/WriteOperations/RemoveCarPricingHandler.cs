using MediatR;
using RentACarProject.Application.Features.Mediator.Commands.CarPricingCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Features.Mediator.Handlers.CarPricingHandlers.WriteOperations
{
    public class RemoveCarPricingCommandHandler : IRequestHandler<RemoveCarPricingCommand>
    {
        private readonly IRepository<CarPricing> _repository;

        public RemoveCarPricingCommandHandler(IRepository<CarPricing> repository)
        {
            _repository = repository;
        }

        #region Metod
        //public async Task Handle(RemoveCarPricingCommand request, CancellationToken cancellationToken)
        //{
        //    var values = await _repository.GetByIdAsync(request.Id);
        //    await _repository.RemoveAsync(values);
        //}
        #endregion Eski

        #region Metod Yeni
        public async Task Handle(RemoveCarPricingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Gelen CarPricingID ile ilgili kaydı al
                var carPricing = await _repository.GetByIdAsync(request.Id);

                // Eğer kayıt bulunursa, CarID'yi al
                if (carPricing != null)
                {
                    int carId = carPricing.CarID; // CarID'yi al

                    // Tüm CarPricing kayıtlarını al
                    var allCarPricingList = await _repository.GetAllAsync();

                    // CarID'ye göre filtrele
                    var carPricingListToRemove = allCarPricingList
                        .Where(cp => cp.CarID == carId)
                        .ToList();

                    // Eğer ilgili kayıtlar varsa sil
                    if (carPricingListToRemove.Any())
                    {
                        foreach (var pricing in carPricingListToRemove)
                        {
                            await _repository.RemoveAsync(pricing);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"CarID '{carId}' ile ilgili CarPricing kayıtları bulunamadı.");
                    }
                }
                else
                {
                    Console.WriteLine($"CarPricingID '{request.Id}' ile kayıt bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Silme işlemi sırasında hata oluştu: {ex.Message}");
            }
        }
        #endregion










    }
}
