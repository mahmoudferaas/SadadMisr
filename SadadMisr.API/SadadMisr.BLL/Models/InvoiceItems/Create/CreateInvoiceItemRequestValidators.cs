using FluentValidation;

namespace SadadMisr.BLL.Models.InvoiceItems.Create
{
    public class CreateInvoiceItemRequestValidators : AbstractValidator<CreateInvoiceItemRequest>
    {
        public CreateInvoiceItemRequestValidators()
        {
            RuleFor(e => e.Data).NotEmpty().NotNull();
            RuleForEach(e => e.Data).ChildRules(ac =>
            {
                ac.RuleFor(a => a.ItemName).NotEmpty().NotNull();
                ac.RuleFor(a => a.ItemAmount).NotEmpty().NotNull();
                ac.RuleFor(a => a.LineInvoiceItemID).NotEmpty().NotNull();
            });
        }
    }
}