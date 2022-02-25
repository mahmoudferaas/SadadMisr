using System.Collections.Generic;

namespace SadadMisr.BLL.Models.Bills.GetById
{
    public class GetBillDetailsByIdsQuery
    {
        public List<long> Ids { get; set; }
    }

    public class GetBillDetailsByIdsOutput
    {
        public List<BillModel> Data { get; set; }
    }
}