using FluentValidation;
using RentACarProject.Application.Features.Mediator.Commands.ReviewCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarProject.Application.Validators.ReviewValidators
{
    public class UpdateReviewValidator : AbstractValidator<UpdateReviewCommand>
    {
        public UpdateReviewValidator()
        {
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage("Please do not leave the customer name empty!");
            RuleFor(x => x.CustomerName).MinimumLength(5).WithMessage("Please enter at least 5 characters!");
            RuleFor(x => x.RatingValue).NotEmpty().WithMessage("Please do not leave the rating value empty");
            RuleFor(x => x.Comment).NotEmpty().WithMessage("Please do not leave the comment value empty");
            RuleFor(x => x.Comment).MinimumLength(50).WithMessage("Please enter at least 50 characters in the comment section");
            RuleFor(x => x.Comment).MaximumLength(500).WithMessage("Please enter at most 500 characters in the comment section");
            RuleFor(x => x.CustomerImage).NotEmpty().WithMessage("Please do not leave the customer image empty!")
                .MinimumLength(10).WithMessage("Please enter at least 10 characters!")
                .MaximumLength(200).WithMessage("Please enter at most 200 characters!");

        }
    }
}
