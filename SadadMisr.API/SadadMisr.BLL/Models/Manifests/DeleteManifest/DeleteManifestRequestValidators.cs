using FluentValidation;

namespace SadadMisr.BLL.Models.DeleteManifest
{
    public class DeleteManifestRequestValidators : AbstractValidator<DeleteManifestRequest>
    {
        public DeleteManifestRequestValidators()
        {
            RuleFor(e => e.Ids).NotEmpty().NotNull();
        }
    }
}