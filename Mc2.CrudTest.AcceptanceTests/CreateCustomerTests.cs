using Mc2.CrudTest.Application.Customer.Commands.CreateCustomer;
using Xunit;
using FluentAssertions;

namespace Mc2.CrudTest.AcceptanceTests
{
    public class BddTddTests
    {
        private readonly CreateCustomerCommandValidator _createCustomerCommandValidator;
        [Fact]
        public void CreateCustomerValid_ReturnsSuccess()
        {
            // Todo: Refer to readme.md 
            var createCustomerCommand = new CreateCustomerCommand
            {
                Firstname = "Ali",
                Lastname = "Jafarpour",
                BankAccountNumber = "1234-2345-3456-4567",
                DateOfBirth = new System.DateTime(1993, 12, 18),
                Email = "a.jafarpour1372@gmail.com",
                PhoneNumber = "09202522976"
            };

            //Act
            var validationResult = _createCustomerCommandValidator.Validate(createCustomerCommand);

            //Assert
            validationResult.IsValid.Should().BeTrue();
        }


    }
}
