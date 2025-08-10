using BankSystem.Domain.Builders;
using BankSystem.Domain.ValueObjects;

namespace BankSystem.Domain.Tests;

public class CustomerTests
{
    [Fact]
    public void CreateCustomer_WithValidParameters_ShouldReturnSuccess()
    {
        string[] ownerName = ["Lucas", "Rocha"];
        var birthDate = BirthDate.Create(new DateTime(2005, 09, 27));
        var document = Document.Create("123.123.123-12");
        var phoneNumber = PhoneNumber.Create("55", "21", "999999999");
        var address = new AddressBuilder()
            .WithCountry("Brazil")
            .WithCity("Rio de Janeiro")
            .WithState("RJ")
            .WithStreetName("Rua")
            .WithHouseNumber(6)
            .WithComplement("Apto 04")
            .Build();
        
        var customer = new CustomerBuilder()
            .WithName(ownerName[0], ownerName[1])
            .WithAddress(address)
            .WithBirthDate(birthDate)
            .WithDocument(document)
            .WithPhoneNumber(phoneNumber)
            .Build();
        
        Assert.Equal($"{ownerName[0]} {ownerName[1]}", customer.OwnerName);
        Assert.Equal(address, customer.Address);
        Assert.Equal(birthDate, customer.BirthDate);
        Assert.Equal(document, customer.Document);
        Assert.Equal(phoneNumber, customer.PhoneNumber);
        Assert.Equal(phoneNumber, customer.PhoneNumber);
    }
}