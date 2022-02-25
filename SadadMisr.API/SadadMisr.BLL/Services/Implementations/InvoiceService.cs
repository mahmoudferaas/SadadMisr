using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SadadMisr.BLL.Common;
using SadadMisr.BLL.Models;
using SadadMisr.BLL.Models.Invoices.Create;
using SadadMisr.BLL.Models.Invoices.Delete;
using SadadMisr.BLL.Models.Invoices.GetById;
using SadadMisr.BLL.Models.Invoices.Update;
using SadadMisr.BLL.Services.Interfaces;
using SadadMisr.DAL;
using SadadMisr.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SadadMisr.BLL.Services.Implementations
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ISadadMasrDbContext _context;
        private readonly IMapper _mapper;

        public InvoiceService(ISadadMasrDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Output<bool>> CreateInvoice(CreateInvoiceRequest request, CancellationToken cancellationToken)
        {
            try
            {
                for (int i = 0; i < request.Data.Count; i++)
                {
                    request.Data[i].CurrencyId = _context.Currencies.Where(x => x.Code == request.Data[i].InvoiceCurrency).ToList()[0].Id;
                    request.Data[i].BillId = _context.Bills.Where(x => x.LineBillId == request.Data[i].BillId).ToList()[0].Id;
                }
                var entities = _mapper.Map<List<Invoice>>(request.Data);

                await _context.Invoices.AddRangeAsync(entities, cancellationToken);

                if (await _context.SaveChangesAsync(cancellationToken) <= 0)
                {
                    return new Output<bool>
                    {
                        Errors = new[] { "DataBaseFailure.." }
                    };
                }
                else
                {
                    try
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                        ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                        using (var clientaa = new HttpClient())
                        {
                            InvoiceAswaq InvoiceAswaq = new InvoiceAswaq();
                            for (int counter = 0; counter < entities.Count; counter++)
                            {
                                InvoiceAswaq.id_pk = int.Parse(entities[counter].Id.ToString());
                                InvoiceAswaq.bill_id = int.Parse(entities[counter].BillId.ToString());
                                InvoiceAswaq.invoice_type_name = entities[counter].InvoiceTypeName;
                                InvoiceAswaq.invoice_number = entities[counter].InvoiceNumber;
                                InvoiceAswaq.issue_date = entities[counter].IssueDate;
                                InvoiceAswaq.invoice_number = entities[counter].InvoiceNumber;
                                InvoiceAswaq.mobile_number = "Mobile";// entities[counter].MobileNumber;
                                InvoiceAswaq.bill_to_party = entities[counter].BillToParty;
                                InvoiceAswaq.tax_number = "tax_number";//entities[counter].TaxNumber;
                                InvoiceAswaq.items_amount = 11;//entities[counter].ItemsAmount;
                                InvoiceAswaq.vat_amount = 12;//entities[counter].VatAmount;
                                InvoiceAswaq.discount_amount = 13;//entities[counter].DiscountAmount;
                                InvoiceAswaq.total_amount = entities[counter].TotalAmount;

                                InvoiceAswaq.invoice_currency = entities[counter].InvoiceCurrency;
                                InvoiceAswaq.is_paid = "IsPAid";// entities[counter].IsPaid.ToString();
                                InvoiceAswaq.line_invoice_id = entities[counter].LineInvoiceId;
                                InvoiceAswaq.IsFixed = "IsFixed";//entities[counter].IsFixed.ToString();

                                InvoiceAswaq.creation_date = entities[counter].CreationDate;
                                InvoiceAswaq.is_sent_to_aswaq = entities[counter].IsSentToAswaq.ToString();
                                InvoiceAswaq.is_done = entities[counter].IsDone.ToString();
                                InvoiceAswaq.status = entities[counter].Status;
                                InvoiceAswaq.is_deleted = "ISdeleted";//entities[counter].IsDeleted.ToString();

                                var jsonstring = JsonConvert.SerializeObject(InvoiceAswaq);
                                var client = new WebClient();
                                client.Headers["Content-type"] = "application/json";
                                client.Encoding = Encoding.UTF8;
                                var content = new StringContent(jsonstring);
                                //string inputJson = (new JavaScriptSerializer()).Serialize(ManifestDto);
                                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                                using (var client2 = new HttpClient())
                                {
                                    var response2 = client2.PostAsync("http://159.89.87.11/sadad/public/api/v1/invoices", content).Result;
                                    if (response2.IsSuccessStatusCode)
                                    {
                                        Console.Write("Success");
                                    }
                                    else
                                        Console.Write("Error");
                                }
                            }

                        }
                    }
                    catch (Exception EXcep)
                    {
                        //Exp = EXcep;
                        //var e = EXcep.Message;
                    }
                }

                return new Output<bool>(true);
            }
            catch (Exception ex)
            {
                var res = new Output<bool>(false);
                res.AddError("Add " + typeof(Invoice).Name, ex.Message);
                return res;
                throw;
            }
        }

        public async Task<Output<bool>> DeleteInvoice(DeleteInvoiceRequest request, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var item in request.Ids)
                {
                    var entity = _context.Invoices
                        .Include(a => a.Payment)
                        .FirstOrDefault(a => a.Id == item);

                    if (entity == null)
                        throw new NotFoundException(nameof(Invoice), item);

                    entity.IsDeleted = true;

                    entity.Payment.IsDeleted = true;

                    await _context.SaveChangesAsync(cancellationToken);
                }

                return new Output<bool>(true);
            }
            catch (Exception ex)
            {
                var res = new Output<bool>(false);
                res.AddError("Update " + typeof(Invoice).Name, ex.Message);
                return res;
                throw;
            }
        }

        public async Task<Output<List<InvoiceModel>>> GetInvoiceByIds(GetInvoiceDetailsByIdsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var dbResult = await _context.Invoices
                        .Where(a => query.Ids.Contains(a.Id))
                        .ToListAsync(cancellationToken);

                return new Output<List<InvoiceModel>>
                {
                    Value = _mapper.Map<List<InvoiceModel>>(dbResult)
                };
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        public async Task<Output<bool>> UpdateInvoice(UpdateInvoiceRequest request, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var item in request.Data)
                {
                   
                    var entityExists = _context.Invoices.Where(M => M.LineInvoiceId == item.LineInvoiceId).ToList();
                    if (entityExists.Count > 0)
                    {
                        item.Id = entityExists[0].Id;
                        item.CurrencyId = entityExists[0].CurrencyId;
                        item.BillId = entityExists[0].BillId;
                    }
                    var entity = _context.Invoices
                       .FirstOrDefault(a => a.Id == item.Id);
                    if (entity == null)
                        throw new NotFoundException(nameof(Invoice), item.Id);

                    _mapper.Map(item, entity);

                    await _context.SaveChangesAsync(cancellationToken);
                }

                return new Output<bool>(true);
            }
            catch (Exception ex)
            {
                var res = new Output<bool>(false);
                res.AddError("Update " + typeof(Invoice).Name, ex.Message);
                return res;
                throw;
            }
        }

        public async Task<Output<List<InvoiceModel>>> GetAllInvoices(CancellationToken cancellationToken)
        {
            try
            {
                var dbResult = await _context.Invoices.ToListAsync(cancellationToken);

                return new Output<List<InvoiceModel>>
                {
                    Value = _mapper.Map<List<InvoiceModel>>(dbResult)
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}