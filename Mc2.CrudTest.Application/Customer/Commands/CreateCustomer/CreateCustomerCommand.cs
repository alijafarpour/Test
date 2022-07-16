using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Mc2.CrudTest.Application.Common.Interfaces;

namespace Mc2.CrudTest.Application.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<int>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Customer
            {
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                BankAccountNumber = request.BankAccountNumber,
            };

            _context.Customers.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
