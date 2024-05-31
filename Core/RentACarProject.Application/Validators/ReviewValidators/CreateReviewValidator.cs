using FluentValidation;
using RentACarProject.Application.Features.Mediator.Commands.ReviewCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Validators.ReviewValidators
{
    public class CreateReviewValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewValidator()
        {
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage("Please fill the Customer Name");
            RuleFor(x => x.CustomerName).MinimumLength(5).WithMessage("Please fill at least 5 charachter");
            RuleFor(x => x.RatingValue).NotEmpty().WithMessage("Please do not leave the rating value empty");
            RuleFor(x => x.Comment).NotEmpty().WithMessage("Please do not leave the comment value empty");
            RuleFor(x => x.Comment).MinimumLength(50).WithMessage("Please enter at least 50 characters in the comment section");
            RuleFor(x => x.Comment).MaximumLength(500).WithMessage("Please enter at most 500 characters in the comment section");

        }
    }
}
