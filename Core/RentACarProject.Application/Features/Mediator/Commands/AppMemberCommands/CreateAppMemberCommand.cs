using MediatR;
using System.ComponentModel.DataAnnotations;

namespace RentACarProject.Application.Features.Mediator.Commands.AppMemberCommands
{
    public class CreateAppMemberCommand : IRequest
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, ErrorMessage = "Username must be between 3 and 30 characters long.", MinimumLength = 3)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(30, ErrorMessage = "Password must be between 6 and 30 characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(30, ErrorMessage = "Name must be between 3 and 30 characters long.", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-ZğĞüÜşŞıİçÇöÖ]*$", ErrorMessage = "Name can only contain letters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        [StringLength(30, ErrorMessage = "Surname must be between 2 and 30 characters long.", MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-ZğĞüÜşŞıİçÇöÖ]*$", ErrorMessage = "Surname can only contain letters.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
    }

}

