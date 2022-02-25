using SadadMisr.BLL.Common;
using SadadMisr.BLL.Models;
using SadadMisr.BLL.Models.Bills.Create;
using SadadMisr.BLL.Models.Bills.Delete;
using SadadMisr.BLL.Models.Bills.GetById;
using SadadMisr.BLL.Models.Bills.Update;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SadadMisr.BLL.Services.Interfaces
{
    public interface IBillService
    {
        Task<Output<bool>> CreateBill(CreateBillRequest request, CancellationToken cancellationToken);

        Task<Output<bool>> UpdateBill(UpdateBillRequest request, CancellationToken cancellationToken);

        Task<Output<bool>> DeleteBill(DeleteBillRequest request, CancellationToken cancellationToken);

        Task<Output<List<BillModel>>> GetBillByIds(GetBillDetailsByIdsQuery query, CancellationToken cancellationToken);

        Task<Output<List<BillModel>>> GetAllBills(CancellationToken cancellationToken);
    }
}