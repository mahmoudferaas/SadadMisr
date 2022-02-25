using SadadMisr.BLL.Common;
using SadadMisr.BLL.Models;
using SadadMisr.BLL.Models.Invoices.Create;
using SadadMisr.BLL.Models.Invoices.Delete;
using SadadMisr.BLL.Models.Invoices.GetById;
using SadadMisr.BLL.Models.Invoices.Update;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SadadMisr.BLL.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<Output<bool>> CreateInvoice(CreateInvoiceRequest request, CancellationToken cancellationToken);

        Task<Output<bool>> UpdateInvoice(UpdateInvoiceRequest request, CancellationToken cancellationToken);

        Task<Output<bool>> DeleteInvoice(DeleteInvoiceRequest request, CancellationToken cancellationToken);

        Task<Output<List<InvoiceModel>>> GetInvoiceByIds(GetInvoiceDetailsByIdsQuery query, CancellationToken cancellationToken);

        Task<Output<List<InvoiceModel>>> GetAllInvoices(CancellationToken cancellationToken);
    }
}