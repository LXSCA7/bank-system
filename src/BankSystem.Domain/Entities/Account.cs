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
            var trimmedAccountNumber = accountNumber.Trim();
            if (string.IsNullOrEmpty(trimmedAccountNumber))
                throw new ArgumentException("Invalid account number", nameof(accountNumber));
            
            if (costumerId == Guid.Empty)
                throw new ArgumentException("Invalid Costumer ID", nameof(costumerId));
            
            AccountNumber = trimmedAccountNumber;
            CostumerId = costumerId;
            Balance = Money.BRL(0);
        }

        public Account Create(string accountNumber, Guid costumerId)
            => new Account(accountNumber, costumerId);
        
        public void Deposit(Money amount) {
            if (amount.Currency != Balance.Currency)
                throw new InvalidOperationException("Moeda do depósito diferente da conta.");

            Balance += amount;
        }

        public void Withdraw(Money amount) {
            if (amount.Currency != Balance.Currency)
                throw new InvalidOperationException("Moeda do saque diferente da conta.");

            if (Balance < amount)
                throw new InsufficientBalanceException(Balance);

            Balance -= amount;
        }
        
    }
}