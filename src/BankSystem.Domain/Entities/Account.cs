using BankSystem.Domain.Exceptions;
using BankSystem.Domain.ValueObjects;

namespace BankSystem.Domain.Entities
{
    public class Account
    {
        private const int MinimumAge = 18;
        public Guid Id { get; private set; }
        public string OwnerName { get; private set; }
        public Money Balance { get; private set; } // value object
        public Document Document { get; private set; } // value object
        private DateTime _birthDate;
        public DateTime BirthDate 
        { 
            get { return _birthDate; } 
            set 
            {
                var minimumDate = DateTime.Now.AddYears(-MinimumAge);
                if (value.Date < minimumDate)
                    throw new ArgumentException("Usuário deve ter pelo menos 18 anos.");    

                _birthDate = value;
            }
        }

        public Account(string name, string document, DateTime birthDate)
        {
            OwnerName = name;
            Balance = Money.BRL(0);
            Document = Document.Create(document);
            BirthDate = birthDate;
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