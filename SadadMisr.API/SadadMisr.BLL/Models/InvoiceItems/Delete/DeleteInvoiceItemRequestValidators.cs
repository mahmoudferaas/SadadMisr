using FluentValidation;

namespace SadadMisr.BLL.Models.InvoiceItems.Delete
{
    public class DeleteInvoiceItemRequestValidators : AbstractValidator<DeleteInvoiceItemRequest>
    {
        public DeleteInvoiceItemRequestValidators()
        {
            RuleFor(e => e.Ids).NotEmpty().NotNull();
        }
    }
}