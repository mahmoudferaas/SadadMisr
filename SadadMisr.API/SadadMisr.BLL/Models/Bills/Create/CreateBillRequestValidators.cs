using FluentValidation;

namespace SadadMisr.BLL.Models.Bills.Create
{
    public class CreateBillRequestValidators : AbstractValidator<CreateBillRequest>
    {
        public CreateBillRequestValidators()
        {
            RuleFor(e => e.Data).NotEmpty().NotNull();
            RuleForEach(e => e.Data).ChildRules(ac =>
            {
                ac.RuleFor(a => a.BillNumber).NotEmpty().NotNull();
            });
        }
    }
}