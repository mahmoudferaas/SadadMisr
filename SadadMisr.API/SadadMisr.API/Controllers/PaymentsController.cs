using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SadadMisr.BLL.Common;
using SadadMisr.BLL.Models;
using SadadMisr.BLL.Models.Payments.Create;
using SadadMisr.BLL.Models.Payments.Delete;
using SadadMisr.BLL.Models.Payments.GetById;
using SadadMisr.BLL.Models.Payments.Update;
using SadadMisr.BLL.Services.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SadadMisr.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _PaymentService;

        public PaymentsController(IPaymentService PaymentService)
        {
            _PaymentService = PaymentService;
        }

        [HttpPost]
        public async Task<Output<bool>> Create([FromBody] CreatePaymentRequest request, CancellationToken cancellationToken)
        {
            return await _PaymentService.CreatePayment(request, cancellationToken);
        }

        [HttpPost("getdetailsbyids")]
        public async Task<Output<List<PaymentModel>>> GetById(GetPaymentDetailsByIdsQuery query, CancellationToken cancellationToken)
        {
            return await _PaymentService.GetPaymentByIds(query, cancellationToken);
        }

        [HttpPut]
        public async Task<Output<bool>> Update([FromBody] UpdatePaymentRequest request, CancellationToken cancellationToken)
        {
            return await _PaymentService.UpdatePayment(request, cancellationToken);
        }

        [HttpDelete]
        public async Task<Output<bool>> Delete([FromBody] DeletePaymentRequest request, CancellationToken cancellationToken)
        {
            return await _PaymentService.DeletePayment(request, cancellationToken);
        }

        [HttpGet]
        public async Task<Output<List<PaymentModel>>> GetAll(CancellationToken cancellationToken)
        {
            return await _PaymentService.GetAllPayments(cancellationToken);
        }
    }
}