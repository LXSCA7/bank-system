using BankSystem.Domain.ValueObjects;

namespace BankSystem.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; private set; }
        public string OwnerName { get; private set; }
        public Money Balance { get; private set; } // value record

        public Account(string name)
        {
            OwnerName = name;
            Balance = Money.BRL(0);
        }

        public void Deposit(Money value) {
            Balance += value;
        }
    }
}