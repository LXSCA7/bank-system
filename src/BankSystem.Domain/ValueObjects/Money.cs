namespace BankSystem.Domain.ValueObjects
{
    public record Money
    {
        public decimal Amount { get; }
        public string Currency { get; }

        private Money(decimal amount, string currency)
        {
            if (amount < 0)
                throw new ArgumentException("O valor não pode ser negativo.", nameof(amount));
            
            if (string.IsNullOrEmpty(currency))
                throw new ArgumentException("A moeda não pode ser vazia.", nameof(currency));
            
            if (currency.Length > 3)
                throw new ArgumentException("A moeda deve conter no máximo 3 caracteres. Exemplo: \"BRL\".", nameof(currency));

            Amount = amount;
            Currency = currency.ToUpper();
        }

        public static Money Create(decimal amount, string currency) => new(amount, currency);
        public static Money BRL(decimal amount) => new(amount, "BRL");
        public static Money USD(decimal amount) => new(amount, "USD");


        public static Money operator +(Money a, Money b) 
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationException("Não é possivel somar moedas diferentes.");

            return new Money(a.Amount + b.Amount, a.Currency);
        }

        public static Money operator -(Money a, Money b) 
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationException("Não é possivel subtrair moedas diferentes.");

            return new Money(a.Amount - b.Amount, a.Currency);
        }

        public static bool operator <(Money a, Money b) => a.Amount < b.Amount && a.Currency == b.Currency;
        public static bool operator >(Money a, Money b) => a.Amount > b.Amount && a.Currency == b.Currency;

      public override string ToString() => $"{Amount} {Currency}";
    }
}