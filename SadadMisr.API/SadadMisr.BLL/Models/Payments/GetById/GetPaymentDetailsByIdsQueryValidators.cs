using FluentValidation;

namespace SadadMisr.BLL.Models.Payments.GetById
{
    public class GetPaymentDetailsByIdsQueryValidators : AbstractValidator<GetPaymentDetailsByIdsQuery>
    {
        public GetPaymentDetailsByIdsQueryValidators()
        {
            RuleFor(e => e.Ids).NotEmpty().NotNull();
        }
    }
}