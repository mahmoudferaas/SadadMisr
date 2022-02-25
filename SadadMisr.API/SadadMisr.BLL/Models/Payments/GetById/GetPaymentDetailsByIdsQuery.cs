using System.Collections.Generic;

namespace SadadMisr.BLL.Models.Payments.GetById
{
    public class GetPaymentDetailsByIdsQuery
    {
        public List<long> Ids { get; set; }
    }

    public class GetPaymentDetailsByIdsOutput
    {
        public List<PaymentModel> Data { get; set; }
    }
}