using Microsoft.AspNetCore.Mvc;
using SadadMisr.BLL.Common;
using SadadMisr.BLL.Models;
using SadadMisr.BLL.Models.Bills.Create;
using SadadMisr.BLL.Models.Bills.Delete;
using SadadMisr.BLL.Models.Bills.GetById;
using SadadMisr.BLL.Models.Bills.Update;
using SadadMisr.BLL.Services.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SadadMisr.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillsController : ControllerBase
    {
        private readonly IBillService _BillService;

        public BillsController(IBillService BillService)
        {
            _BillService = BillService;
        }

        [HttpPost]
        public async Task<Output<bool>> Create([FromBody] CreateBillRequest request, CancellationToken cancellationToken)
        {
            return await _BillService.CreateBill(request, cancellationToken);
        }

        [HttpPost("getdetailsbyids")]
        public async Task<Output<List<BillModel>>> GetById(GetBillDetailsByIdsQuery query, CancellationToken cancellationToken)
        {
            return await _BillService.GetBillByIds(query, cancellationToken);
        }

        [HttpPut]
        public async Task<Output<bool>> Update([FromBody] UpdateBillRequest request, CancellationToken cancellationToken)
        {
            return await _BillService.UpdateBill(request, cancellationToken);
        }

        [HttpDelete]
        public async Task<Output<bool>> Delete([FromBody] DeleteBillRequest request, CancellationToken cancellationToken)
        {
            return await _BillService.DeleteBill(request, cancellationToken);
        }

        [HttpGet]
        public async Task<Output<List<BillModel>>> GetAll(CancellationToken cancellationToken)
        {
            return await _BillService.GetAllBills(cancellationToken);
        }
    }
}