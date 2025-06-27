namespace BankSystem.Domain.ValueObjects;

public record Address
{
    public string Country { get; }
    public string City    { get; }
    public string State   { get; }
    public string StreetName { get; }
    public int HouseNumber { get; }
    public string? Complement { get;  }

    private Address(string country, string city, string state, string streetName, int houseNumber, string? complement)
    {
        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("País não pode ser vazio.", nameof(country));
        
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("Cidade não pode ser vazia.", nameof(city));
        
        if (string.IsNullOrWhiteSpace(state))
            throw new ArgumentException("Estado não pode ser vazio.", nameof(state));
        
        if (state.Length != 2)
            throw new ArgumentOutOfRangeException("Estado deve possuir no maximo duas letras.", nameof(state));
        
        if (houseNumber < 1)
            throw new ArgumentOutOfRangeException("Numero de casa invalido.", nameof(houseNumber));
        
        
        Country = country;
        City = city;
        State = state;
        StreetName = streetName;
        HouseNumber = houseNumber;
        Complement = complement;
    }
    
    public static Address Create(string country, string city, string state, string streetName, int houseNumber, string? complement)
        => new (country, city, state, streetName, houseNumber, complement);
    
    public static Address CreateBrazil(string city, string state, string streetName, int houseNumber, string? complement)
        => new ("Brazil", city, state, streetName, houseNumber, complement);
    
    public override string ToString()
        =>  $"{StreetName}, {HouseNumber} {(string.IsNullOrEmpty(Complement) ? "" : $",{Complement}")}, " +
                $"{City} - {State}, {Country}";
}