using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SadadMisr.BLL.Common;
using SadadMisr.BLL.Models;
using SadadMisr.BLL.Models.InvoiceItems.Create;
using SadadMisr.BLL.Models.InvoiceItems.Delete;
using SadadMisr.BLL.Models.InvoiceItems.GetById;
using SadadMisr.BLL.Models.InvoiceItems.Update;
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
    public class InvoiceItemService : IInvoiceItemService
    {
        private readonly ISadadMasrDbContext _context;
        private readonly IMapper _mapper;

        public InvoiceItemService(ISadadMasrDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Output<bool>> CreateInvoiceItem(CreateInvoiceItemRequest request, CancellationToken cancellationToken)
        {
            try
            {
                for (int i = 0; i < request.Data.Count; i++)
                {
                    request.Data[i].InvoiceId = _context.Invoices.Where(x => x.LineInvoiceId == request.Data[i].LineInvoiceId).ToList()[0].Id;
                }
                var entities = _mapper.Map<List<InvoiceItem>>(request.Data);

                await _context.InvoiceItems.AddRangeAsync(entities, cancellationToken);

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
                        for (int i = 0; i < request.Data.Count; i++)
                        {
                            using (var clientaa = new HttpClient())
                            {
                                InvoiceItemAswaq InvoiceItemAswaq = new InvoiceItemAswaq();
                                {
                                    InvoiceItemAswaq.id_pk = int.Parse(entities[i].Id.ToString());
                                    InvoiceItemAswaq.invoice_id = entities[i].InvoiceId;
                                    InvoiceItemAswaq.line_invoice_item_id = entities[i].LineInvoiceItemID;
                                    InvoiceItemAswaq.item_name = entities[i].ItemName;
                                    InvoiceItemAswaq.item_amount = entities[i].ItemAmount;
                                    InvoiceItemAswaq.item_order = entities[i].ItemOrder;
                                    InvoiceItemAswaq.created_at = DateTime.Now;
                                    //InvoiceItemAswaq.updated_at = DateTime.Now;
                                    //InvoiceItemAswaq.deleted_at = DateTime.Now;
                                    InvoiceItemAswaq.is_deleted = 0;
                                    //id id_pk  invoice_id line_invoice_item_id item_name item_amount item_order is_deleted created_at updated_at deleted_at
                                    var jsonstring = JsonConvert.SerializeObject(InvoiceItemAswaq);
                                    var client = new WebClient();
                                    client.Headers["Content-type"] = "application/json";
                                    client.Encoding = Encoding.UTF8;
                                    var content = new StringContent(jsonstring);
                                    //string inputJson = (new JavaScriptSerializer()).Serialize(ManifestDto);
                                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                                    using (var client2 = new HttpClient())
                                    {
                                        var response2 = client2.PostAsync("http://159.89.87.11/sadad/public/api/v1/invoice_items", content).Result;
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
                res.AddError("Add " + typeof(InvoiceItem).Name, ex.Message);
                return res;
                throw;
            }
        }

        public async Task<Output<bool>> DeleteInvoiceItem(DeleteInvoiceItemRequest request, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var item in request.Ids)
                {
                    var entity = _context.InvoiceItems
                        .FirstOrDefault(a => a.Id == item);

                    if (entity == null)
                        throw new NotFoundException(nameof(InvoiceItem), item);

                    entity.IsDeleted = true;

                    await _context.SaveChangesAsync(cancellationToken);
                }

                return new Output<bool>(true);
            }
            catch (Exception ex)
            {
                var res = new Output<bool>(false);
                res.AddError("Update " + typeof(InvoiceItem).Name, ex.Message);
                return res;
                throw;
            }
        }

        public async Task<Output<List<InvoiceItemModel>>> GetInvoiceItemByIds(GetInvoiceItemDetailsByIdsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var dbResult = await _context.InvoiceItems
                        .Where(a => query.Ids.Contains(a.Id))
                        .ToListAsync(cancellationToken);

                return new Output<List<InvoiceItemModel>>
                {
                    Value = _mapper.Map<List<InvoiceItemModel>>(dbResult)
                };
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        public async Task<Output<bool>> UpdateInvoiceItem(UpdateInvoiceItemRequest request, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var item in request.Data)
                {
                    
                    var entityExists = _context.InvoiceItems.Where(M => M.LineInvoiceItemID == item.LineInvoiceItemID).ToList();
                    if (entityExists.Count > 0)
                    {
                        item.Id = entityExists[0].Id;
                        item.InvoiceId = entityExists[0].InvoiceId;
                        item.LineInvoiceItemID = entityExists[0].LineInvoiceItemID;
                    }
                    var entity = _context.InvoiceItems
                       .FirstOrDefault(a => a.Id == item.Id);
                    if (entity == null)
                        throw new NotFoundException(nameof(InvoiceItem), item.Id);

                    _mapper.Map(item, entity);

                    await _context.SaveChangesAsync(cancellationToken);
                }

                return new Output<bool>(true);
            }
            catch (Exception ex)
            {
                var res = new Output<bool>(false);
                res.AddError("Update " + typeof(InvoiceItem).Name, ex.Message);
                return res;
                throw;
            }
        }

        public async Task<Output<List<InvoiceItemModel>>> GetAllInvoiceItems(CancellationToken cancellationToken)
        {
            try
            {
                var dbResult = await _context.InvoiceItems.ToListAsync(cancellationToken);

                return new Output<List<InvoiceItemModel>>
                {
                    Value = _mapper.Map<List<InvoiceItemModel>>(dbResult)
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}