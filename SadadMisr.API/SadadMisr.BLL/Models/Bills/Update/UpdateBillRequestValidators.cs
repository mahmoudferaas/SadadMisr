using FluentValidation;


namespace SadadMisr.BLL.Models.Bills.Update
{
    public class UpdateBillRequestValidators : AbstractValidator<UpdateBillRequest>
    {
        public UpdateBillRequestValidators()
        {
            RuleFor(e => e.Data).NotEmpty().NotNull();
            RuleForEach(e => e.Data).ChildRules(ac =>
            {
                //ac.RuleFor(a => a.Id).NotEmpty().NotNull();
                ac.RuleFor(a => a.LineBillId).NotEmpty().NotNull();
                ac.RuleFor(a => a.BillNumber).NotEmpty().NotNull();
            });
        }
    }
}
