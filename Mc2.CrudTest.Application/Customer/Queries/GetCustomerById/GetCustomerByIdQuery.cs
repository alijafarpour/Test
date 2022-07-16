using Mc2.CrudTest.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Customer.Queries.GetCustomerById
{
    public record GetCustomerByIdQuery : IRequest<Domain.Entities.Customer>
    {
        public int Id { get; set; }
    }

    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Domain.Entities.Customer>
    {
        private readonly IApplicationDbContext _context;
        public GetCustomerByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Customer> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
        {
            var Customer = _context.Customers.Where(a => a.Id == query.Id).FirstOrDefault();
            if (Customer == null) return null;

            return Customer;
        }
    }
}
