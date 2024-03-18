using BookCollect.Services.ViewModels.Authentications;
using FluentValidation;

namespace BookCollect.Services.Validation
{
    public class RegisterValidation : AbstractValidator<RegisterVM>
    {
        public RegisterValidation()
        {
            RuleFor( p => p.UserName )
                .NotNull()
                .NotEmpty()
                .WithMessage("UserName is required");

            RuleFor(p => p.Password)
                .NotEmpty()
                .NotNull()
                .WithMessage("Password is required");

            RuleFor(p => p.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Password is required");
        }
    }
}
