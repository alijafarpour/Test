using Mc2.CrudTest.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Customer.Queries.GetAllCustomer
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<Domain.Entities.Customer>>
    {

    }

    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<Domain.Entities.Customer>>
    {
        private readonly IApplicationDbContext _context;
        public GetAllCustomersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Domain.Entities.Customer>> Handle(GetAllCustomersQuery query, CancellationToken cancellationToken)
        {
            var CustomerList = await _context.Customers.Where(w => !w.IsDeleted).ToListAsync();
            if (CustomerList.Count == 0)
                return null;

            return CustomerList.AsReadOnly();
        }
    }
}
