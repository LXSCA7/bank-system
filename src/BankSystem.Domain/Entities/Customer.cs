using System.Data.Common;
using System.Runtime.ExceptionServices;
using System.Transactions;
using BankSystem.Domain.ValueObjects;

namespace BankSystem.Domain.Entities;

public class Customer : Entity
{
    public string OwnerName        { get; }
    public Address Address          { get; private set; }
    public Document Document       { get; }
    public BirthDate BirthDate     { get; }
    public PhoneNumber PhoneNumber { get; private set; }
    
    internal Customer(string ownerName, Address address,  Document document, BirthDate birthDate, PhoneNumber phoneNumber)
    {
        OwnerName = ownerName;
        PhoneNumber = phoneNumber;
        Document = document;
        BirthDate = birthDate;
        Address = address;
    }

    public void UpdateAddress(Address address)
    {
        Address = address;
        Update();
    }

    public void UpdatePhoneNumber(PhoneNumber phoneNumber)
    {
        PhoneNumber = phoneNumber;
        Update();
    }
}