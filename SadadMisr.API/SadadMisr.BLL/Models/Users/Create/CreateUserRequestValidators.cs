using FluentValidation;

namespace SadadMisr.BLL.Models.Users.Create
{
    public class CreateUserRequestValidators : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidators()
        {
            RuleFor(e => e.Data).NotEmpty().NotNull();
            RuleForEach(e => e.Data).ChildRules(ac =>
            {
                ac.RuleFor(a => a.UserName).NotEmpty().NotNull();
                ac.RuleFor(a => a.Email).NotEmpty().NotNull().EmailAddress();
                ac.RuleFor(a => a.Password).NotEmpty().NotNull();
            });
        }
    }
}