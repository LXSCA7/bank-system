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
        public BirthDate BirthDate { get; private set; }
        public Password Password { get; private set; }

        public Account(string name, string document, DateTime birthDate, string plainTextPassword)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome do titular não pode estar em branco.", nameof(name));
            OwnerName = name;
            Balance = Money.BRL(0);
            Document = Document.Create(document);
            BirthDate = BirthDate.Create(birthDate);
            Password = Password.Create(plainTextPassword);
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