using BankSystem.Domain.Exceptions;
using BankSystem.Domain.ValueObjects;

namespace BankSystem.Domain.Entities
{
    public class Account : Entity
    {
        public string AccountNumber { get; }
        public Money Balance { get; private set; }
        public Guid CostumerId { get; }
        private Account(string accountNumber, Guid costumerId)
        {
            var validateAccountNumber = ValidateAccountNumber(accountNumber);
            
            if (costumerId == Guid.Empty)
                throw new ArgumentException("Invalid Costumer ID", nameof(costumerId));
            
            AccountNumber = validateAccountNumber;
            CostumerId = costumerId;
            Balance = Money.BRL(0);
        }

        public static Account Create(string accountNumber, Guid costumerId)
            => new Account(accountNumber, costumerId);
        
        public void Deposit(Money amount) 
        {
            if (amount.Currency != Balance.Currency)
                throw new InvalidOperationException("Moeda do depósito diferente da conta.");

            if (amount.Amount <= 0)
                throw new InvalidOperationException("It's not possible deposit values <= 0.");
            
            Balance += amount;
        }

        public void Withdraw(Money amount) 
        {
            if (amount.Currency != Balance.Currency)
                throw new InvalidOperationException("Moeda do saque diferente da conta.");

            if (amount.Amount <= 0)
                throw new InvalidOperationException("It's not possible withdraw values <= 0.");
            
            if (Balance < amount)
                throw new InsufficientBalanceException(Balance);

            Balance -= amount;
        }

        private static string ValidateAccountNumber(string accountNumber)
        {
            const int maxLength = 10;
            var trimmedAccountNumber = accountNumber.Trim();
            if (string.IsNullOrEmpty(trimmedAccountNumber))
                throw new ArgumentException("Invalid account number: " +
                                            "Account number can't be null or empty.", nameof(accountNumber));
            
            if (trimmedAccountNumber.Length > maxLength)
                throw new ArgumentException("Invalid account number: " +
                                            $"Account number lenght needs to be <= {maxLength}.", nameof(accountNumber));
            
            if (trimmedAccountNumber.Any(c => !char.IsDigit(c)))
                throw new ArgumentException("Invalid account number: " +
                                            $"Account number can only have digits.", nameof(accountNumber));
            
            return trimmedAccountNumber.PadLeft(maxLength, '0');
        }
    }
}