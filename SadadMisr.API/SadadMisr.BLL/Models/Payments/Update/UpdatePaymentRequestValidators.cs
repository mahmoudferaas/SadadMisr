using FluentValidation;


namespace SadadMisr.BLL.Models.Payments.Update
{
    public class UpdatePaymentRequestValidators : AbstractValidator<UpdatePaymentRequest>
    {
        public UpdatePaymentRequestValidators()
        {
            RuleFor(e => e.Data).NotEmpty().NotNull();
            RuleForEach(e => e.Data).ChildRules(ac =>
            {
                ac.RuleFor(a => a.Id).NotEmpty().NotNull();
                ac.RuleFor(a => a.PaymentDate).NotEmpty().NotNull();
                ac.RuleFor(a => a.TransactionId).NotEmpty().NotNull();
                ac.RuleFor(a => a.TransactionNumber).NotEmpty().NotNull();
            });
        }
    }
}
