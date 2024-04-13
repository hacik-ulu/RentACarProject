using RentACarProject.Application.Features.CQRS.Commands.CategoryCommands;
using RentACarProject.Application.Interfaces.GeneralInterfaces;
using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Features.CQRS.Handlers.CategoryHandlers
{
    public class UpdateCategoryCommandHandler
    {
        private readonly IRepository<Category> _repository;
        public UpdateCategoryCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }
        public async Task Handle(UpdateCategoryCommand command)
        {
            try
            {
                var values = await _repository.GetByIdAsync(command.CategoryID);
                if (values == null)
                {
                    Console.WriteLine("Category not found!"); // Kategori bulunamadıysa konsola yazdır
                    return;
                }

                values.Name = command.Name;
                await _repository.UpdateAsync(values);
                Console.WriteLine("Category Updated!"); // Kategori güncellendiğinde konsola yazdır
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}"); // Bir hata oluşursa konsola yazdır
            }
        }

    }
}