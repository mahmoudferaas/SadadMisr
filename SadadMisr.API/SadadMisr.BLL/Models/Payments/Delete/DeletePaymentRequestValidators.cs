using FluentValidation;

namespace SadadMisr.BLL.Models.Payments.Delete
{
    public class DeletePaymentRequestValidators : AbstractValidator<DeletePaymentRequest>
    {
        public DeletePaymentRequestValidators()
        {
            RuleFor(e => e.Ids).NotEmpty().NotNull();
        }
    }
}