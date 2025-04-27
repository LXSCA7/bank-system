using BankSystem.Domain.Exceptions;
using BankSystem.Domain.ValueObjects;

namespace BankSystem.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; private set; }
        public string OwnerName { get; private set; }
        public Money Balance { get; private set; } // value object
        public Document Document { get; private set; } // value object

        public Account(string name, string document)
        {
            OwnerName = name;
            Balance = Money.BRL(0);
            Document = Document.Create(document);
        }

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