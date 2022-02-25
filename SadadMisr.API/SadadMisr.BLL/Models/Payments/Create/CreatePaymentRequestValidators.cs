using FluentValidation;

namespace SadadMisr.BLL.Models.Payments.Create
{
    public class CreatePaymentRequestValidators : AbstractValidator<CreatePaymentRequest>
    {
        public CreatePaymentRequestValidators()
        {
            RuleFor(e => e.Data).NotEmpty().NotNull();
            RuleForEach(e => e.Data).ChildRules(ac =>
            {
                ac.RuleFor(a => a.PaymentDate).NotEmpty().NotNull();
                ac.RuleFor(a => a.TotalAmount).NotEmpty().NotNull();
                ac.RuleFor(a => a.TransactionId).NotEmpty().NotNull();
                ac.RuleFor(a => a.TransactionNumber).NotEmpty().NotNull();
            });
        }
    }
}