using Microsoft.AspNetCore.Mvc;
using SadadMisr.BLL.Common;
using SadadMisr.BLL.Models;
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
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _InvoiceService;

        public InvoicesController(IInvoiceService InvoiceService)
        {
            _InvoiceService = InvoiceService;
        }

        [HttpPost]
        public async Task<Output<bool>> Create([FromBody] CreateInvoiceRequest request, CancellationToken cancellationToken)
        {
            return await _InvoiceService.CreateInvoice(request, cancellationToken);
        }

        [HttpPost("getdetailsbyids")]
        public async Task<Output<List<InvoiceModel>>> GetById(GetInvoiceDetailsByIdsQuery query, CancellationToken cancellationToken)
        {
            return await _InvoiceService.GetInvoiceByIds(query, cancellationToken);
        }

        [HttpPut]
        public async Task<Output<bool>> Update([FromBody] UpdateInvoiceRequest request, CancellationToken cancellationToken)
        {
            return await _InvoiceService.UpdateInvoice(request, cancellationToken);
        }

        [HttpDelete]
        public async Task<Output<bool>> Delete([FromBody] DeleteInvoiceRequest request, CancellationToken cancellationToken)
        {
            return await _InvoiceService.DeleteInvoice(request, cancellationToken);
        }

        [HttpGet]
        public async Task<Output<List<InvoiceModel>>> GetAll(CancellationToken cancellationToken)
        {
            return await _InvoiceService.GetAllInvoices(cancellationToken);
        }
    }
}