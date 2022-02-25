using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SadadMisr.BLL.Common;
using SadadMisr.BLL.Models;
using SadadMisr.BLL.Models.Bills.Create;
using SadadMisr.BLL.Models.Bills.Delete;
using SadadMisr.BLL.Models.Bills.GetById;
using SadadMisr.BLL.Models.Bills.Update;
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
    public class BillService : IBillService
    {
        private readonly ISadadMasrDbContext _context;
        private readonly IMapper _mapper;

        public BillService(ISadadMasrDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Output<bool>> CreateBill(CreateBillRequest request, CancellationToken cancellationToken)
        {
            try
            {
                for (int i = 0; i < request.Data.Count; i++)
                {
                    request.Data[i].ShippingLineId = _context.ShippingLines.Where(x => x.Code == request.Data[i].ShippingLineCode).ToList()[0].Id;
                    var Manifest = await _context.Manifests.Where(m => m.LineManifestId == request.Data[i].ManifestId).ToListAsync();
                    
                    if(Manifest.Count > 0)
                        request.Data[i].ManifestId = Manifest[0].Id;
                }
                var entities = _mapper.Map<List<Bill>>(request.Data);

                await _context.Bills.AddRangeAsync(entities, cancellationToken);

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
                            BillAswaq BillAswaq = new BillAswaq();
                            for (int counter = 0; counter < entities.Count; counter++)
                            {
                                BillAswaq.id_pk = int.Parse(entities[counter].Id.ToString());
                                BillAswaq.manifest_id = int.Parse(entities[counter].ManifestId.ToString());
                                BillAswaq.shipping_line_id = entities[counter].ShippingLineId;
                                BillAswaq.bill_number = entities[counter].BillNumber;
                                BillAswaq.number_of_containers = int.Parse(entities[counter].NumberOfContainers.ToString());
                                BillAswaq.pol = entities[counter].POL;
                                BillAswaq.pod = entities[counter].POD;
                                BillAswaq.aci_dnumber = entities[counter].ACIDnumber;
                                BillAswaq.customer_id = entities[counter].CustomerId;
                                BillAswaq.customer_tax_number = entities[counter].CustomerTaxNumber;
                                BillAswaq.customer_mobile_number = entities[counter].CustomerMobileNumber;
                                BillAswaq.scac = entities[counter].SCAC;
                                BillAswaq.customer_name = entities[counter].CustomerName;
                                BillAswaq.line_bill_id = entities[counter].LineBillId;

                                BillAswaq.creation_date = entities[counter].CreationDate;
                                BillAswaq.is_sent_to_aswaq = entities[counter].IsSentToAswaq.ToString();
                                BillAswaq.is_done = entities[counter].IsDone.ToString();
                                BillAswaq.status = entities[counter].Status;
                                BillAswaq.is_deleted = entities[counter].IsDeleted.ToString();

                                BillAswaq.containers_20 = entities[counter].Containers20;
                                BillAswaq.containers_40 = entities[counter].Containers40;
                                BillAswaq.customer_email = entities[counter].CustomerEmail;

                                var jsonstring = JsonConvert.SerializeObject(BillAswaq);
                                var client = new WebClient();
                                client.Headers["Content-type"] = "application/json";
                                client.Encoding = Encoding.UTF8;
                                var content = new StringContent(jsonstring);
                                //string inputJson = (new JavaScriptSerializer()).Serialize(ManifestDto);
                                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                                using (var client2 = new HttpClient())
                                {
                                    var response2 = client2.PostAsync("http://159.89.87.11/sadad/public/api/v1/bills", content).Result;
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
                res.AddError("Add " + typeof(Bill).Name, ex.Message);
                return res;
                throw;
            }
        }

        public async Task<Output<bool>> DeleteBill(DeleteBillRequest request, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var item in request.Ids)
                {
                    var entity = _context.Bills
                        .Include(a => a.Invoices)
                        .ThenInclude(a => a.Payment)
                        .FirstOrDefault(a => a.Id == item);

                    if (entity == null)
                        throw new NotFoundException(nameof(Bill), item);

                    entity.IsDeleted = true;
                    foreach (var invoice in entity.Invoices)
                    {
                        invoice.IsDeleted = true;
                        invoice.Payment.IsDeleted = true;
                    }

                    await _context.SaveChangesAsync(cancellationToken);
                }

                return new Output<bool>(true);
            }
            catch (Exception ex)
            {
                var res = new Output<bool>(false);
                res.AddError("Update " + typeof(Bill).Name, ex.Message);
                return res;
                throw;
            }
        }

        public async Task<Output<List<BillModel>>> GetBillByIds(GetBillDetailsByIdsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var dbResult = await _context.Bills
                        .Where(a => query.Ids.Contains(a.Id))
                        .ToListAsync(cancellationToken);

                return new Output<List<BillModel>>
                {
                    Value = _mapper.Map<List<BillModel>>(dbResult)
                };
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        public async Task<Output<bool>> UpdateBill(UpdateBillRequest request, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var item in request.Data)
                {
                    var entityExists = _context.Bills.Where(M => M.LineBillId == item.LineBillId).ToList(); 
                    if (entityExists.Count > 0)
                    {
                        item.Id = entityExists[0].Id;
                        item.LineBillId = entityExists[0].LineBillId;
                        item.ManifestId = entityExists[0].ManifestId;
                        item.ShippingLineId = entityExists[0].ShippingLineId;
                    }

                    var entity = _context.Bills
                        .FirstOrDefault(a => a.Id == item.Id);

                    if (entity == null)
                        throw new NotFoundException(nameof(Bill), item.Id);

                    _mapper.Map(item, entity);

                   //await _context.SaveChangesAsync(cancellationToken);






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
                                BillAswaq BillAswaq = new BillAswaq();
                                // (int counter = 0; counter < entities.Count; counter++)
                                {
                                    BillAswaq.id_pk = int.Parse(entity.Id.ToString());
                                    BillAswaq.manifest_id = int.Parse(entity.ManifestId.ToString());
                                    BillAswaq.shipping_line_id = entity.ShippingLineId;
                                    BillAswaq.bill_number = entity.BillNumber;
                                    BillAswaq.number_of_containers = int.Parse(entity.NumberOfContainers.ToString());
                                    BillAswaq.pol = entity.POL;
                                    BillAswaq.pod = entity.POD;
                                    BillAswaq.aci_dnumber = entity.ACIDnumber;
                                    BillAswaq.customer_id = entity.CustomerId;
                                    BillAswaq.customer_tax_number = entity.CustomerTaxNumber;
                                    BillAswaq.customer_mobile_number = entity.CustomerMobileNumber;
                                    BillAswaq.scac = entity.SCAC;
                                    BillAswaq.customer_name = entity.CustomerName;
                                    BillAswaq.line_bill_id = entity.LineBillId;

                                    BillAswaq.creation_date = entity.CreationDate;
                                    BillAswaq.is_sent_to_aswaq = entity.IsSentToAswaq.ToString();
                                    BillAswaq.is_done = entity.IsDone.ToString();
                                    BillAswaq.status = entity.Status;
                                    BillAswaq.is_deleted = entity.IsDeleted.ToString();

                                    BillAswaq.containers_20 = entity.Containers20;
                                    BillAswaq.containers_40 = entity.Containers40;
                                    BillAswaq.customer_email = entity.CustomerEmail;

                                    var jsonstring = JsonConvert.SerializeObject(BillAswaq);
                                    var client = new WebClient();
                                    client.Headers["Content-type"] = "application/json";
                                    client.Encoding = Encoding.UTF8;
                                    var content = new StringContent(jsonstring);
                                    //string inputJson = (new JavaScriptSerializer()).Serialize(ManifestDto);
                                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                                    using (var client2 = new HttpClient())
                                    {
                                        var response2 = client2.PutAsync("http://159.89.87.11/sadad/public/api/v1/bills/"+ BillAswaq.id_pk, content).Result;
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






                }

                return new Output<bool>(true);
            }
            catch (Exception ex)
            {
                var res = new Output<bool>(false);
                res.AddError("Update " + typeof(Bill).Name, ex.Message);
                return res;
                throw;
            }
        }

        public async Task<Output<List<BillModel>>> GetAllBills(CancellationToken cancellationToken)
        {
            try
            {
                var dbResult = await _context.Bills.ToListAsync(cancellationToken);

                return new Output<List<BillModel>>
                {
                    Value = _mapper.Map<List<BillModel>>(dbResult)
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}