using FluentValidation;

namespace SadadMisr.BLL.Models.Invoices.Delete
{
    public class DeleteInvoiceRequestValidators : AbstractValidator<DeleteInvoiceRequest>
    {
        public DeleteInvoiceRequestValidators()
        {
            RuleFor(e => e.Ids).NotEmpty().NotNull();
        }
    }
}