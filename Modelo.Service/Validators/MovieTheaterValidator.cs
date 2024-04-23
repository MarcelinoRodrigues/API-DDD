using FluentValidation;
using Modelo.Domain.Entities;

namespace Modelo.Service.Validators
{
    public class MovieTheaterValidator : AbstractValidator<MovieTheater>
    {
        public MovieTheaterValidator()
        {
            RuleFor(c => c.Number)
                .NotEmpty().WithMessage("Please enter the Number.")
                .NotNull().WithMessage("Please enter the Number.");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Please enter the Description.")
                .NotNull().WithMessage("Please enter the Description.");

            RuleFor(c => c.Movies)
                .NotEmpty().WithMessage("Please enter the Movie.")
                .NotNull().WithMessage("Please enter the Movie.");
        }
    }
}
