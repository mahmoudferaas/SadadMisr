using FluentValidation;


namespace SadadMisr.BLL.Models.Invoices.Update
{
    public class UpdateInvoiceRequestValidators : AbstractValidator<UpdateInvoiceRequest>
    {
        public UpdateInvoiceRequestValidators()
        {
            RuleFor(e => e.Data).NotEmpty().NotNull();
            RuleForEach(e => e.Data).ChildRules(ac =>
            {
                ac.RuleFor(a => a.LineInvoiceId).NotEmpty().NotNull();
                ac.RuleFor(a => a.InvoiceNumber).NotEmpty().NotNull();
            });
        }
    }
}
