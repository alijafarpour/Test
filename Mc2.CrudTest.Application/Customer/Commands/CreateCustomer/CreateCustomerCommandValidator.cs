using FluentValidation;
using Mc2.CrudTest.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using libphonenumber;
using System.Text.RegularExpressions;

namespace Mc2.CrudTest.Application.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateCustomerCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress()
                .MustAsync(BeUniqueEmail).WithMessage("The specified Email already exists.");

            RuleFor(v => v.Firstname)
             .NotEmpty().WithMessage("Firstname is required.")
             .MaximumLength(200).WithMessage("Firstname must not exceed 200 characters.");

            RuleFor(v => v.Lastname)
             .NotEmpty().WithMessage("Lastname is required.")
             .MaximumLength(200).WithMessage("Lastname must not exceed 200 characters.");

            RuleFor(v => v.DateOfBirth)
             .NotNull().WithMessage("Lastname is required.");

            RuleFor(v => v.PhoneNumber)
               .NotEmpty().WithMessage("PhoneNumber is required.")
               .MustAsync(CheckPhoneNumberValidation).WithMessage("PhoneNumber is not valid");

            RuleFor(v => v.BankAccountNumber)
              .MustAsync(CheckBankAccountValidation).WithMessage("BankAccountNumber is not valid");
        }

        public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Customers
                .AllAsync(l => l.Email != email, cancellationToken);
        }

        public async Task<bool> CheckPhoneNumberValidation(string phoneNumber, CancellationToken cancellationToken)
        {
            var isValid = PhoneNumberUtil.Instance.Parse(phoneNumber, null);
            return  isValid.IsValidNumber;
        }

        public async Task<bool> CheckBankAccountValidation(string accountNumber, CancellationToken cancellationToken)
        {
            return Regex.IsMatch(accountNumber, "((\\d{4})-){3}\\d{4}");
        }
    }

}
