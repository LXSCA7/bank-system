using System.Runtime.ExceptionServices;
using System.Transactions;
using BankSystem.Domain.ValueObjects;

namespace BankSystem.Domain.Entities;

public class Customer : Entity
{
    public string OwnerName        { get; }
    public Address Address          { get; }
    public Document Document       { get; }
    public BirthDate BirthDate     { get; }
    public PhoneNumber PhoneNumber { get; }

    private Customer(string firstName, string lastName, string countryCode, string ddd, 
                     string phoneNumber, string documentString, DateTime birthDate,
                     string country, string city, string state, string street, int houseNumber, string complement)
    {
        OwnerName = $"{firstName} {lastName}";
        PhoneNumber = PhoneNumber.Create(countryCode, ddd, phoneNumber);
        Document = Document.Create(documentString);
        BirthDate = BirthDate.Create(birthDate);
        Address = Address.Create(country,city, state, street, houseNumber, complement);
    }
    
    public Customer Create(string firstName, string lastName, string countryCode, string ddd, 
                string phoneNumber, string documentString, DateTime birthDate,
                string country, string city, string state, string street, int houseNumber, string complement)
        => new(firstName, lastName, countryCode, ddd, phoneNumber, 
                documentString, birthDate, country, city, state, street, houseNumber, complement);
}