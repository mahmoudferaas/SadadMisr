using System.Collections.Generic;

namespace SadadMisr.BLL.Models.Invoices.GetById
{
    public class GetInvoiceDetailsByIdsQuery
    {
        public List<long> Ids { get; set; }
    }

    public class GetInvoiceDetailsByIdsOutput
    {
        public List<InvoiceModel> Data { get; set; }
    }
}