using FluentValidation;

namespace SadadMisr.BLL.Models.InvoiceItems.GetById
{
    public class GetInvoiceItemDetailsByIdsQueryValidators : AbstractValidator<GetInvoiceItemDetailsByIdsQuery>
    {
        public GetInvoiceItemDetailsByIdsQueryValidators()
        {
            RuleFor(e => e.Ids).NotEmpty().NotNull();
        }
    }
}