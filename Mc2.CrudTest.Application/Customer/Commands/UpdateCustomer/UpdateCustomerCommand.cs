using Mc2.CrudTest.Application.Common.Exceptions;
using Mc2.CrudTest.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Customer.Commands.UpdateCustomer
{
    public record UpdateCustomerCommand : IRequest
    {
        public int Id { get; init; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Customers
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Customer), request.Id);
            }

            entity.Firstname = request.Firstname;
            entity.Lastname = request.Lastname;
            entity.DateOfBirth = request.DateOfBirth;
            entity.PhoneNumber = request.PhoneNumber;
            entity.Email = request.Email;
            entity.BankAccountNumber = request.BankAccountNumber;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
