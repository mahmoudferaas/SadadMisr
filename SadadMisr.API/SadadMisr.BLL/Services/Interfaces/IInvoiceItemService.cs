using SadadMisr.BLL.Common;
using SadadMisr.BLL.Models;
using SadadMisr.BLL.Models.Bills.Create;
using SadadMisr.BLL.Models.Bills.Delete;
using SadadMisr.BLL.Models.Bills.GetById;
using SadadMisr.BLL.Models.Bills.Update;
using SadadMisr.BLL.Models.InvoiceItems.Create;
using SadadMisr.BLL.Models.InvoiceItems.Delete;
using SadadMisr.BLL.Models.InvoiceItems.GetById;
using SadadMisr.BLL.Models.InvoiceItems.Update;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SadadMisr.BLL.Services.Interfaces
{
    public interface IInvoiceItemService
    {
        Task<Output<bool>> CreateInvoiceItem(CreateInvoiceItemRequest request, CancellationToken cancellationToken);

        Task<Output<bool>> UpdateInvoiceItem(UpdateInvoiceItemRequest request, CancellationToken cancellationToken);

        Task<Output<bool>> DeleteInvoiceItem(DeleteInvoiceItemRequest request, CancellationToken cancellationToken);

        Task<Output<List<InvoiceItemModel>>> GetInvoiceItemByIds(GetInvoiceItemDetailsByIdsQuery query, CancellationToken cancellationToken);

        Task<Output<List<InvoiceItemModel>>> GetAllInvoiceItems(CancellationToken cancellationToken);
    }
}