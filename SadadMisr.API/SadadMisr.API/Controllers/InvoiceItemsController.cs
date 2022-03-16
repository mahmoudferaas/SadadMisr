using Microsoft.AspNetCore.Mvc;
using SadadMisr.BLL.Common;
using SadadMisr.BLL.Models;
using SadadMisr.BLL.Models.InvoiceItems.Create;
using SadadMisr.BLL.Models.InvoiceItems.Delete;
using SadadMisr.BLL.Models.InvoiceItems.GetById;
using SadadMisr.BLL.Models.InvoiceItems.Update;
using SadadMisr.BLL.Models.Invoices.Create;
using SadadMisr.BLL.Models.Invoices.Delete;
using SadadMisr.BLL.Models.Invoices.GetById;
using SadadMisr.BLL.Models.Invoices.Update;
using SadadMisr.BLL.Services.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SadadMisr.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceItemsController : ControllerBase
    {
        private readonly IInvoiceItemService _InvoiceItemService;

        public InvoiceItemsController(IInvoiceItemService InvoiceService)
        {
            _InvoiceItemService = InvoiceService;
        }

        [HttpPost]
        public async Task<Output<bool>> Create([FromBody] CreateInvoiceItemRequest request, CancellationToken cancellationToken)
        {
            return await _InvoiceItemService.CreateInvoiceItem(request, cancellationToken);
        }

        [HttpPost("getdetailsbyids")]
        public async Task<Output<List<InvoiceItemModel>>> GetById(GetInvoiceItemDetailsByIdsQuery query, CancellationToken cancellationToken)
        {
            return await _InvoiceItemService.GetInvoiceItemByIds(query, cancellationToken);
        }

        [HttpPut]
        public async Task<Output<bool>> Update([FromBody] UpdateInvoiceItemRequest request, CancellationToken cancellationToken)
        {
            return await _InvoiceItemService.UpdateInvoiceItem(request, cancellationToken);
        }

        [HttpDelete]
        public async Task<Output<bool>> Delete([FromBody] DeleteInvoiceItemRequest request, CancellationToken cancellationToken)
        {
            return await _InvoiceItemService.DeleteInvoiceItem(request, cancellationToken);
        }

        [HttpGet]
        public async Task<Output<List<InvoiceItemModel>>> GetAll(CancellationToken cancellationToken)
        {
            return await _InvoiceItemService.GetAllInvoiceItems(cancellationToken);
        }
    }
}