namespace BankSystem.Domain.ValueObjects;

public record BirthDate
{
    private const int MINIMUM_AGE = 18;
    public DateTime Value { get; }
    private BirthDate(DateTime value) => Value = value;

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
}