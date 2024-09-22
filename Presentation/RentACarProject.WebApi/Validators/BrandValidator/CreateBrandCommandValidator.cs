using FluentValidation;
using RentACarProject.Application.Features.CQRS.Commands.BrandCommands;
using RentACarProject.Domain.Entities;
using RentACarProject.Dto.BrandDtos;
using RentACarProject.Persistence.Context;

namespace RentACarProject.WebApi.Validators.BrandValidator
{
    public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty.")
                .Length(2, 25).WithMessage("Name must be between 2 and 25 characters long.")
                .Must(UniqueName).WithMessage("Brand Name already exists.");
        }

        // Unique Control
        private bool UniqueName(string name)
        {
            using (var context = new RentACarContext())
            {
                var dbBrand = context.Brands.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
                if (dbBrand == null)
                {
                    return true; // Unique Brand Name
                }
                else
                {
                    return false; // Not Unique Brand Name
                }
            }
        }
    }
}
