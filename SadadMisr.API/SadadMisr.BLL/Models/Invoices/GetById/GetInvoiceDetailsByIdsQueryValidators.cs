using FluentValidation;

namespace SadadMisr.BLL.Models.Invoices.GetById
{
    public class GetInvoiceDetailsByIdsQueryValidators : AbstractValidator<GetInvoiceDetailsByIdsQuery>
    {
        public GetInvoiceDetailsByIdsQueryValidators()
        {
            RuleFor(e => e.Ids).NotEmpty().NotNull();
        }
    }
}