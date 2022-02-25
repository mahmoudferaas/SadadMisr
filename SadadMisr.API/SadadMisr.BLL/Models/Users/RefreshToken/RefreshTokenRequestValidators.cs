using FluentValidation;

namespace SadadMisr.BLL.Models.Users.RefreshToken
{
    public class RefreshTokenRequestValidators : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidators()
        {
            RuleFor(a => a.AccessToken).NotEmpty().NotNull();
            RuleFor(a => a.RefreshToken).NotEmpty().NotNull();
        }
    }
}