using FluentValidation;
using SadadMisr.BLL.Models.GetByIds;
using SadadMisr.BLL.Models.Manifests.GetByIds;

namespace SadadMisr.BLL.Models.Manifests
{
    public class GetDetailsByIdsQueryValidators : AbstractValidator<GetDetailsByIdsQuery>
    {
        public GetDetailsByIdsQueryValidators()
        {
            RuleFor(e => e.Ids).NotEmpty().NotNull();
        }
    }
}
