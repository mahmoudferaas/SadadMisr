using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SadadMisr.BLL.Common;
using SadadMisr.BLL.Models;
using SadadMisr.BLL.Models.Payments.Create;
using SadadMisr.BLL.Models.Payments.Delete;
using SadadMisr.BLL.Models.Payments.GetById;
using SadadMisr.BLL.Models.Payments.Update;
using SadadMisr.BLL.Services.Interfaces;
using SadadMisr.DAL;
using SadadMisr.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SadadMisr.BLL.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly ISadadMasrDbContext _context;
        private readonly IMapper _mapper;

        public PaymentService(ISadadMasrDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Output<bool>> CreatePayment(CreatePaymentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entities = _mapper.Map<List<Payment>>(request.Data);

                await _context.Payments.AddRangeAsync(entities, cancellationToken);

                if (await _context.SaveChangesAsync(cancellationToken) <= 0)
                {
                    return new Output<bool>
                    {
                        Errors = new[] { "DataBaseFailure.." }
                    };
                }

                return new Output<bool>(true);
            }
            catch (Exception ex)
            {
                var res = new Output<bool>(false);
                res.AddError("Add " + typeof(Payment).Name, ex.Message);
                return res;
                throw;
            }
        }

        public async Task<Output<bool>> DeletePayment(DeletePaymentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var item in request.Ids)
                {
                    var entity = _context.Payments.FirstOrDefault(a => a.Id == item);

                    if (entity == null)
                        throw new NotFoundException(nameof(Payment), item);

                    entity.IsDeleted = true;

                    await _context.SaveChangesAsync(cancellationToken);
                }

                return new Output<bool>(true);
            }
            catch (Exception ex)
            {
                var res = new Output<bool>(false);
                res.AddError("Update " + typeof(Payment).Name, ex.Message);
                return res;
                throw;
            }
        }

        public async Task<Output<List<PaymentModel>>> GetPaymentByIds(GetPaymentDetailsByIdsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var dbResult = await _context.Payments
                        .Where(a => query.Ids.Contains(a.Id))
                        .ToListAsync(cancellationToken);

                return new Output<List<PaymentModel>>
                {
                    Value = _mapper.Map<List<PaymentModel>>(dbResult)
                };
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        public async Task<Output<bool>> UpdatePayment(UpdatePaymentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var item in request.Data)
                {
                    var entity = _context.Payments
                        .FirstOrDefault(a => a.Id == item.Id);

                    if (entity == null)
                        throw new NotFoundException(nameof(Payment), item.Id);

                    _mapper.Map(item, entity);

                   await _context.SaveChangesAsync(cancellationToken);
                }

                return new Output<bool>(true);
            }
            catch (Exception ex)
            {
                var res = new Output<bool>(false);
                res.AddError("Update " + typeof(Payment).Name, ex.Message);
                return res;
                throw;
            }
        }

        public async Task<Output<List<PaymentModel>>> GetAllPayments(CancellationToken cancellationToken)
        {
            try
            {
                var dbResult = await _context.Payments.ToListAsync(cancellationToken);

                return new Output<List<PaymentModel>>
                {
                    Value = _mapper.Map<List<PaymentModel>>(dbResult)
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}