using Mc2.CrudTest.Application.Common.Exceptions;
using Mc2.CrudTest.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Customer.Commands.DeleteCustomer
{
    public record DeleteCustomerCommand(int Id) : IRequest;

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Customers
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Customer), request.Id);
            }

            entity.IsDeleted = true;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
