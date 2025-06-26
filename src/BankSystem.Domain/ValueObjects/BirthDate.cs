namespace BankSystem.Domain.ValueObjects;

public record BirthDate
{
    private const int MINIMUM_AGE = 18;
    public DateTime Value { get; }
    private BirthDate(DateTime value) => Value = value;
    
    /// <summary>
    /// Creates a BirthDate instance.
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="ArgumentException">Lançada se a data for invalida.</exception>
    public static BirthDate Create(DateTime value)
    {
        value = value.Date;
        Validate(value);
        return new(value);
    }
    
    private static void Validate(DateTime value)
    {
        var today = DateTime.Today.Date;
        var minimumDate = today.AddYears(-MINIMUM_AGE);
        if (value > minimumDate)
            throw new ArgumentException("Usuário deve ter pelo menos 18 anos.");
    }   
    
    public virtual bool Equals(BirthDate? other)
        => other is not null && other.Value == Value;

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public virtual bool Equals(DateTime? other)
        => other == Value;

    public static bool operator ==(DateTime? a, BirthDate b)
        => a == b.Value;
    
    public static bool operator !=(DateTime? a, BirthDate b)
        => !(a == b);
    
    public static bool operator ==(BirthDate a, DateTime b)
        => a.Value == b;
    
    public static bool operator !=(BirthDate a, DateTime b)
        => !(a.Value == b);

}