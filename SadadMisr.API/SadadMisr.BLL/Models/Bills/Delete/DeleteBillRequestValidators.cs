using FluentValidation;

namespace SadadMisr.BLL.Models.Bills.Delete
{
    public class DeleteBillRequestValidators : AbstractValidator<DeleteBillRequest>
    {
        public DeleteBillRequestValidators()
        {
            RuleFor(e => e.Ids).NotEmpty().NotNull();
        }
    }
}