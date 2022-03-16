using FluentValidation;


namespace SadadMisr.BLL.Models.InvoiceItems.Update
{
    public class UpdateInvoiceItemRequestValidators : AbstractValidator<UpdateInvoiceItemRequest>
    {
        public UpdateInvoiceItemRequestValidators()
        {
            RuleFor(e => e.Data).NotEmpty().NotNull();
            RuleForEach(e => e.Data).ChildRules(ac =>
            {
                //ac.RuleFor(a => a.LineInvoiceId).NotEmpty().NotNull();
                //ac.RuleFor(a => a.InvoiceNumber).NotEmpty().NotNull();
            });
        }
    }
}
