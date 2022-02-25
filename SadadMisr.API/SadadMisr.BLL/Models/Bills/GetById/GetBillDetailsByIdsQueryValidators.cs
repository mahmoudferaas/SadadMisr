using FluentValidation;

namespace SadadMisr.BLL.Models.Bills.GetById
{
    public class GetBillDetailsByIdsQueryValidators : AbstractValidator<GetBillDetailsByIdsQuery>
    {
        public GetBillDetailsByIdsQueryValidators()
        {
            RuleFor(e => e.Ids).NotEmpty().NotNull();
        }
    }
}