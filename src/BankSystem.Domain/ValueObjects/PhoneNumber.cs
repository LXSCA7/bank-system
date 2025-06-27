using System.Security.Principal;

namespace BankSystem.Domain.ValueObjects;

public record PhoneNumber
{
    public string CountryCode { get; }
    public string Ddd         { get; }
    public string Number      { get; }

    private PhoneNumber(string countryCode, string ddd, string number)
    {
        countryCode = countryCode.Replace("+", "");
        Verify(countryCode, ddd, number);
        
        CountryCode = countryCode;
        Ddd = ddd;
        Number = number.Replace("-", "");
    }

    public static PhoneNumber Create(string countryCode, string ddd, string number)
        => new(countryCode, ddd, number);
    
    public static PhoneNumber CreateBrazilian(string ddd, string number)
        => new ("55", ddd, number);

    private void Verify(string countryCode, string ddd, string number)
    {
        if (countryCode.Length > 3)
            throw new ArgumentOutOfRangeException("O codigo do pais nao pode ser maior que 3 digitos.", nameof(countryCode));
        
        if (string.Equals(countryCode, "55") && !number.StartsWith("9"))
            throw new ArgumentOutOfRangeException("Numeros brasileiros devem comecar com 9.", nameof(number));
            
    }

    public override string ToString()
        => $"+{CountryCode} {Ddd} {Number}";
}