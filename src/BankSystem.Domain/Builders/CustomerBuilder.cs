using BankSystem.Domain.Entities;
using BankSystem.Domain.ValueObjects;

namespace BankSystem.Domain.Builders;

public class CustomerBuilder
{
    private static string _ownerName;
    private static Address _address;
    private static Document _document;
    private static BirthDate _birthDate; 
    private static PhoneNumber _phoneNumber;

    public CustomerBuilder WithName(string firstName, string lastName)
    {
        _ownerName = $"{firstName} {lastName}";
        return this;
    }

    public CustomerBuilder WithAddress(Address address)
    {
        _address = address;
        return this;
    }
    
    public CustomerBuilder WithDocument(Document document)
    {
        _document = document;
        return this;
    }

    public CustomerBuilder WithBirthDate(BirthDate birthDate)
    {
        _birthDate = birthDate;
        return this;
    }

    public CustomerBuilder WithPhoneNumber(PhoneNumber phoneNumber)
    {
        _phoneNumber = phoneNumber;
        return this;
    }

    public Customer Build()
        => new Customer(_ownerName, _address, _document, _birthDate, _phoneNumber);
}