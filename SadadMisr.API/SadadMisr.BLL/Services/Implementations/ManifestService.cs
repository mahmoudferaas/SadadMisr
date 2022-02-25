using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SadadMisr.BLL.Common;
using SadadMisr.BLL.Models;
using SadadMisr.BLL.Models.CreateManifest;
using SadadMisr.BLL.Models.DeleteManifest;
using SadadMisr.BLL.Models.GetByIds;
using SadadMisr.BLL.Models.Manifests.GetByIds;
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
    public class ManifestService : IManifestService
    {
        private readonly ISadadMasrDbContext _context;
        private readonly IMapper _mapper;

        public ManifestService(ISadadMasrDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Output<bool>> CreateManifest(CreateManifestRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //ShippingLineCode
                for (int i = 0; i < request.Data.Count; i++)
                {
                    request.Data[i].ShippingLineId = _context.ShippingLines.Where(x => x.Code == request.Data[i].ShippingLineCode).ToList()[0].Id;
                    request.Data[i].ShippingAgencyId = _context.ShippingAgencies.Where(x => x.Code == request.Data[i].ShippingAgencyCode).ToList()[0].Id;
                }

                var entities = _mapper.Map<List<Manifest>>(request.Data);
                
                await _context.Manifests.AddRangeAsync(entities, cancellationToken);

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
                            ManifestAswaq ManifestAswaq = new ManifestAswaq();
                            for (int counter = 0; counter < entities.Count; counter++)
                            {
                                ManifestAswaq.id_pk = int.Parse(entities[counter].Id.ToString());
                                ManifestAswaq.vessel_name = entities[counter].VesselName;
                                ManifestAswaq.voyage_number = entities[counter].VoyageNumber;
                                ManifestAswaq.estimated_date = entities[counter].EstimatedDate;
                                ManifestAswaq.shipping_agency_id = int.Parse(entities[counter].ShippingAgencyId.ToString());
                                ManifestAswaq.line_manifest_id = entities[counter].LineManifestId;
                                ManifestAswaq.call_port = entities[counter].CallPort;
                                ManifestAswaq.number_of_bills = entities[counter].NumberOfBills;
                                ManifestAswaq.is_export = entities[counter].IsExport.ToString();
                                ManifestAswaq.shipping_line_id = int.Parse(entities[counter].ShippingLineId.ToString());
                                ManifestAswaq.is_sent_to_aswaq = entities[counter].IsSentToAswaq.ToString();
                                ManifestAswaq.is_done = entities[counter].IsDone.ToString();
                                ManifestAswaq.status = entities[counter].Status;
                                ManifestAswaq.is_deleted = entities[counter].IsDeleted.ToString();


                                var jsonstring = JsonConvert.SerializeObject(ManifestAswaq);
                                var client = new WebClient();
                                client.Headers["Content-type"] = "application/json";
                                client.Encoding = Encoding.UTF8;
                                var content = new StringContent(jsonstring);
                                //string inputJson = (new JavaScriptSerializer()).Serialize(ManifestDto);
                                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                                using (var client2 = new HttpClient())
                                {
                                    var response2 = client2.PostAsync("http://159.89.87.11/sadad/public/api/v1/manifists", content).Result;
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
                res.AddError("Add " + typeof(Manifest).Name, ex.Message);
                return res;
                throw;
            }
        }

        public async Task<Output<bool>> DeleteManifest(DeleteManifestRequest request, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var item in request.Ids)
                {
                    var entity = _context.Manifests
                        .Include(a => a.Bills)
                        .ThenInclude(a => a.Invoices)
                        .ThenInclude(a => a.Payment)
                        .FirstOrDefault(a => a.Id == item);

                    if (entity == null)
                        throw new NotFoundException(nameof(Manifest), item);

                    entity.IsDeleted = true;
                    foreach (var bill in entity.Bills)
                    {
                        bill.IsDeleted = true;
                        foreach (var invoice in bill.Invoices)
                        {
                            invoice.IsDeleted = true;
                            invoice.Payment.IsDeleted = true;
                        }
                    }

                    await _context.SaveChangesAsync(cancellationToken);
                }

                return new Output<bool>(true);
            }
            catch (Exception ex)
            {
                var res = new Output<bool>(false);
                res.AddError("Update " + typeof(Manifest).Name, ex.Message);
                return res;
                throw;
            }
        }

        public async Task<Output<List<ManifestModel>>> GetManifestByIds(GetDetailsByIdsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var dbResult = await _context.Manifests
                        .Where(a => query.Ids.Contains(a.Id))
                        //.Include(a => a.Bills)
                        //.ThenInclude(a => a.Invoices)
                        //.ThenInclude(a => a.Payment)
                        .ToListAsync(cancellationToken);

                return new Output<List<ManifestModel>>
                {
                    Value = _mapper.Map<List<ManifestModel>>(dbResult)
                };
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        public async Task<Output<bool>> UpdateManifest(UpdateManifestRequest request, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var item in request.Data)
                {
                    var entityExists = _context.Manifests.Where(M => M.LineManifestId == item.LineManifestId).ToList();
                    if(entityExists.Count > 0)
                    {
                        item.Id = entityExists[0].Id;
                        item.ShippingAgencyId = entityExists[0].ShippingAgencyId;
                        item.ShippingLineId = entityExists[0].ShippingLineId;
                    }
                    var entity = _context.Manifests
                        //.Include(a => a.Bills)
                        //.ThenInclude(a => a.Invoices)
                        //.ThenInclude(a => a.Payment)
                        .FirstOrDefault(a => a.Id == item.Id);

                    if (entity == null)
                        throw new NotFoundException(nameof(Manifest), item.Id);

                    _mapper.Map(item, entity);

                   //await _context.SaveChangesAsync(cancellationToken);





                    if (await _context.SaveChangesAsync(cancellationToken) <= 0)
                    {
                    }
                    else
                    {
                        try
                        {
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                            using (var clientaa = new HttpClient())
                            {
                                ManifestAswaq ManifestAswaq = new ManifestAswaq();
                                //for (int counter = 0; counter < entity.Count; counter++)
                                {
                                    ManifestAswaq.id_pk = int.Parse(entity.Id.ToString());
                                    ManifestAswaq.vessel_name = entity.VesselName;
                                    ManifestAswaq.voyage_number = entity.VoyageNumber;
                                    ManifestAswaq.estimated_date = entity.EstimatedDate;
                                    ManifestAswaq.shipping_agency_id = int.Parse(entity.ShippingAgencyId.ToString());
                                    ManifestAswaq.line_manifest_id = entity.LineManifestId;
                                    ManifestAswaq.call_port = entity.CallPort;
                                    ManifestAswaq.number_of_bills = entity.NumberOfBills;
                                    ManifestAswaq.is_export = entity.IsExport.ToString();
                                    ManifestAswaq.shipping_line_id = int.Parse(entity.ShippingLineId.ToString());
                                    ManifestAswaq.is_sent_to_aswaq = entity.IsSentToAswaq.ToString();
                                    ManifestAswaq.is_done = entity.IsDone.ToString();
                                    ManifestAswaq.status = entity.Status;
                                    ManifestAswaq.is_deleted = entity.IsDeleted.ToString();

                                    var jsonstring = JsonConvert.SerializeObject(ManifestAswaq);
                                    var client = new WebClient();
                                    client.Headers["Content-type"] = "application/json";
                                    client.Encoding = Encoding.UTF8;
                                    var content = new StringContent(jsonstring);
                                    //string inputJson = (new JavaScriptSerializer()).Serialize(ManifestDto);
                                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                                    using (var client2 = new HttpClient())
                                    {
                                        var response2 = client2.PutAsync(("http://159.89.87.11/sadad/public/api/v1/manifists/"+ ManifestAswaq.id_pk), content).Result;
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
                        }
                    }




                }

                return new Output<bool>(true);
            }
            catch (Exception ex)
            {
                var res = new Output<bool>(false);
                res.AddError("Update " + typeof(Manifest).Name, ex.Message);
                return res;
                throw;
            }
        }

        public async Task<Output<List<ManifestModel>>> GetAllManifests(CancellationToken cancellationToken)
        {
            try
            {
                var dbResult = await _context.Manifests.ToListAsync(cancellationToken);

                return new Output<List<ManifestModel>>
                {
                    Value = _mapper.Map<List<ManifestModel>>(dbResult)
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}