using BankSystem.Domain.ValueObjects;

namespace BankSystem.Domain.Entities;

public class Transaction
{
    public Guid Id { get; }
    public Guid AccountId { get; }
    public Guid? RelatedAccountId { get;  }
    public Money Amount { get; }
    public DateTime TransactionDate { get; }
    public TransactionType TransactionType { get; }
    public string? Description { get; }

    private Transaction(Guid accountId, Guid? relatedAccountId, Money amount, TransactionType transactionType, string? description)
    {
        if (!relatedAccountId.HasValue && transactionType == TransactionType.Transfer)
            throw new InvalidOperationException("Uma transacao deve conter o ID da conta relacionada.");
        
        if (amount.Amount <= 0)
            throw new InvalidOperationException("O valor deve ser maior que zero.");
        
        Id = Guid.NewGuid();
        AccountId = accountId;
        RelatedAccountId = relatedAccountId;
        Amount = amount;
        TransactionDate = DateTime.Now;
        TransactionType = transactionType;
        Description = description;
    }

    public static Transaction CreateDeposit(Money amount, Guid accountId, string? description)
        => new Transaction(accountId, null, amount, TransactionType.Deposit, description);
         
    public static Transaction CreateWithdraw(Money amount, Guid accountId, string? description)
        => new Transaction(accountId, null, amount, TransactionType.Withdraw, description);
        
   public static Transaction CreatePayment(Guid accountId, Money value, string? description)
       => new Transaction(accountId, null, value, TransactionType.Payment, description);
   
   public static Transaction CreateTransfer(Guid accountId, Guid relatedAccountId, Money value, string? description) 
       => new Transaction(accountId,  relatedAccountId, value, TransactionType.Transfer, description);
}

public enum TransactionType
{
    Transfer,
    Withdraw,
    Deposit,
    Payment,
}