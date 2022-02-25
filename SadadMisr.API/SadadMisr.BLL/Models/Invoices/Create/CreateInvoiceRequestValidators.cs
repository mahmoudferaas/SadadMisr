using FluentValidation;

namespace SadadMisr.BLL.Models.Invoices.Create
{
    public class CreateInvoiceRequestValidators : AbstractValidator<CreateInvoiceRequest>
    {
        public CreateInvoiceRequestValidators()
        {
            RuleFor(e => e.Data).NotEmpty().NotNull();
            RuleForEach(e => e.Data).ChildRules(ac =>
            {
                ac.RuleFor(a => a.InvoiceNumber).NotEmpty().NotNull();
                ac.RuleFor(a => a.IssueDate).NotEmpty().NotNull();
                ac.RuleFor(a => a.TotalAmount).NotEmpty().NotNull();
            });
        }
    }
}