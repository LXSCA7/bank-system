using System.Net.Http.Headers;
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
            get => _birthDate; 
            private set 
            {
                var today = DateTime.Today;
                var minimumDate = today.AddYears(-MinimumAge);
                if (value.Date > minimumDate)
                    throw new ArgumentException("Usuário deve ter pelo menos 18 anos.");
                _birthDate = value;
            }
        }
        private string _password;
        public required string Password 
        {
            get => _password;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Senha não pode ser vazia.");
                if (value.Length < 8)
                    throw new WeakPasswordException("Senha deve conter ao menos 8 caracteres.");
                if (!value.Any(char.IsDigit))
                    throw new WeakPasswordException("Senha deve conter ao menos um número.");
                if (!value.Any(char.IsAsciiLetterUpper))
                    throw new WeakPasswordException("Senha deve conter ao menos uma letra maiúscula");
                if (!value.Any(char.IsAsciiLetterLower))
                    throw new WeakPasswordException("Senha deve conter ao menos uma letra minúscula.");
                
                // falta usar BCrypt: 
                _password = value ?? throw new ArgumentException("Senha não pode ser vazia.");
            }
        } 

        public Account(string name, string document, DateTime birthDate, string password)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome do titular não estar em branco.", nameof(name));
            OwnerName = name;
            Balance = Money.BRL(0);
            Document = Document.Create(document);
            BirthDate = birthDate;
            Password = password;
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