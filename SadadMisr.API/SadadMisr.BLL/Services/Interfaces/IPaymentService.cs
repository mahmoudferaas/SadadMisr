using SadadMisr.BLL.Common;
using SadadMisr.BLL.Models;
using SadadMisr.BLL.Models.Payments.Create;
using SadadMisr.BLL.Models.Payments.Delete;
using SadadMisr.BLL.Models.Payments.GetById;
using SadadMisr.BLL.Models.Payments.Update;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SadadMisr.BLL.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<Output<bool>> CreatePayment(CreatePaymentRequest request, CancellationToken cancellationToken);

        Task<Output<bool>> UpdatePayment(UpdatePaymentRequest request, CancellationToken cancellationToken);

        Task<Output<bool>> DeletePayment(DeletePaymentRequest request, CancellationToken cancellationToken);

        Task<Output<List<PaymentModel>>> GetPaymentByIds(GetPaymentDetailsByIdsQuery query, CancellationToken cancellationToken);

        Task<Output<List<PaymentModel>>> GetAllPayments(CancellationToken cancellationToken);
    }
}