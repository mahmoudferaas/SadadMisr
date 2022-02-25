using FluentValidation;

namespace SadadMisr.BLL.Models.Users.Login
{
    public class LoginRequestValidators : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidators()
        {
            RuleFor(a => a.Email).NotEmpty().NotNull();
            RuleFor(a => a.Password).NotEmpty().NotNull();
        }
    }
}