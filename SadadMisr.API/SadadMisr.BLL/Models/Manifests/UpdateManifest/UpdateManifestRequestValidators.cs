using FluentValidation;


namespace SadadMisr.BLL.Models.CreateManifest
{
    public class UpdateManifestRequestValidators : AbstractValidator<UpdateManifestRequest>
    {
        public UpdateManifestRequestValidators()
        {
            RuleFor(e => e.Data).NotEmpty().NotNull();
            RuleForEach(e => e.Data).ChildRules(ac =>
            {
                ac.RuleFor(a => a.LineManifestId).NotEmpty().NotNull();
                ac.RuleFor(a => a.VesselName).NotEmpty().NotNull();
                //ac.RuleForEach(s => s.Bills).ChildRules(s =>
                //{
                //    s.RuleFor(a => a.BillNumber).NotEmpty().NotNull();
                //    s.RuleFor(a => a.BillNumber).NotEmpty().NotNull();
                //    s.RuleForEach(s => s.Invoices).ChildRules(d =>
                //    {
                //        d.RuleFor(a => a.InvoiceNumber).NotEmpty().NotNull();
                //        d.RuleFor(a => a.IssueDate).NotEmpty().NotNull();
                //        d.RuleFor(a => a.TotalAmount).NotEmpty().NotNull();
                //        d.RuleFor(a => a.TotalAmount).NotEmpty().NotNull();
                //    });
                //});
            });
        }
    }
}
