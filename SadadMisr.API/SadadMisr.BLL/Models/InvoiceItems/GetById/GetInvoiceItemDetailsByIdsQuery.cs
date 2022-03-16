using System.Collections.Generic;

namespace SadadMisr.BLL.Models.InvoiceItems.GetById
{
    public class GetInvoiceItemDetailsByIdsQuery
    {
        public List<long> Ids { get; set; }
    }

    public class GetInvoiceDetailsByIdsOutput
    {
        public List<InvoiceItemModel> Data { get; set; }
    }
}