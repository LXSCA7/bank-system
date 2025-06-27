using System.Text.RegularExpressions;

namespace BankSystem.Domain.ValueObjects;

public record Email
{
    public string Value { get; }
    private static readonly Regex EmailRegex = new Regex(
        @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);
    
    private Email(string value)
    {
        string trimmedValue = value.Trim();
        if (string.IsNullOrEmpty(trimmedValue))
            throw new ArgumentException("Endereco de email nao pode ser vazio ou em branco.", nameof(value));
        
        if (!EmailRegex.IsMatch(trimmedValue))
            throw new ArgumentException("Endereco de email invalido.", nameof(value));
        
        Value = trimmedValue;
    }

    public static Email Create(string value)
        => new(value);
    
    public override  string ToString() => Value;
}