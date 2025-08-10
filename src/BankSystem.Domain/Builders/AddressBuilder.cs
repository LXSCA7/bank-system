using System.Runtime.InteropServices;
using BankSystem.Domain.ValueObjects;

namespace BankSystem.Domain.Builders;

public class AddressBuilder
{
    private static string _country;
    private static string _city;
    private static string _state;
    private static string _streetName;
    private static int _houseNumber;
    private static string? _complement = null;

    public AddressBuilder WithCountry(string country)
    {
        _country = country;
        return this;
    }

    public AddressBuilder WithCity(string city)
    {
        _city = city;
        return this;
    }

    public AddressBuilder WithState(string state)
    {
        _state = state;
        return this;
    }

    public AddressBuilder WithStreetName(string streetName)
    {
        _streetName = streetName;
        return this;
    }

    public AddressBuilder WithHouseNumber(int houseNumber)
    {
        _houseNumber = houseNumber;
        return this;
    }

    public AddressBuilder WithComplement(string complement)
    {
        _complement = complement;
        return this;
    }
    
    public Address Build()
    {
        return Address.Create(_country, _city, _state, _streetName, _houseNumber, _complement);
    }
}