using BankSystem.Domain.Entities;
using BankSystem.Domain.ValueObjects;

namespace BankSystem.Domain.Tests;

public class TransactionTests
{
    [Fact]
    public void CreateTransferTransaction_WithInvalidAmount_ShouldThrowException()
    {
        var accountId = Guid.NewGuid();
        var relatedAccountId = Guid.NewGuid();
        var money = Money.BRL(0M); 
        const string description = "Salario.";
        
        var ex = Assert.Throws<InvalidOperationException>(() => 
                Transaction.CreateTransfer(accountId, relatedAccountId, money, description));
        
        Assert.Equal("O valor deve ser maior que zero.", ex.Message);
    }

    [Fact]
    public void CreateTransferTransaction_WithEqualsAccountId_ShouldThrowException()
    {
        var accountId = Guid.NewGuid();
        var money = Money.BRL(4500.0M);
        const string description = "Salario";
        
        var ex = Assert.Throws<InvalidOperationException>(() => 
            Transaction.CreateTransfer(accountId, accountId, money, description));
        
        Assert.Equal("Voce nao pode fazer uma transferencia para si mesmo.", ex.Message);
    }

    [Fact]
    public void CreateTransferTransaction_WithValidParameters_ShouldReturnSuccess()
    {
        var accountId = Guid.NewGuid();
        var relatedAccountId = Guid.NewGuid();
        var money = Money.BRL(4500.0M);
        const string description = "Salario";
        
        var transaction = Transaction.CreateTransfer(accountId, relatedAccountId, money, description);
        
        Assert.Equal(money, transaction.Amount);
        Assert.Equal(description, transaction.Description);
        Assert.Equal(accountId, transaction.AccountId);
        Assert.Equal(relatedAccountId, transaction.RelatedAccountId);
        Assert.Equal(TransactionType.Transfer, transaction.TransactionType);
    }
    
    [Fact]
    public void CreateTransferTransaction_WithValidParametersAndNullDescription_ShouldReturnSuccess()
    {
        var accountId = Guid.NewGuid();
        var relatedAccountId = Guid.NewGuid();
        var money = Money.BRL(4500.0M);
        
        var transaction = Transaction.CreateTransfer(accountId, relatedAccountId, money);
        
        Assert.Equal(money, transaction.Amount);
        Assert.Null(transaction.Description);
        Assert.Equal(accountId, transaction.AccountId);
        Assert.Equal(relatedAccountId, transaction.RelatedAccountId);
        Assert.Equal(TransactionType.Transfer, transaction.TransactionType);
    }
    
    [Fact]
    public void CreateDepositTransaction_WithInvalidAmount_ShouldThrowException()
    {
        var accountId = Guid.NewGuid();
        var money = Money.BRL(0M);
        const string description = "Deposito do salario da conta XPTO para essa.";
        
        var ex = Assert.Throws<InvalidOperationException>(() 
            => Transaction.CreateDeposit(accountId, money, description));
        
        Assert.Equal("O valor deve ser maior que zero.", ex.Message);
    }
    
    [Fact]
    public void CreateDepositTransaction_WithValidAmount_ShouldReturnSuccess()
    {
        var accountId = Guid.NewGuid();
        var money = Money.BRL(1500.0M);
        const string description = "Deposito do salario da conta XPTO para essa.";
        
        var transaction = Transaction.CreateDeposit(accountId, money, description);
        
        Assert.Equal(money, transaction.Amount);
        Assert.Equal(accountId, transaction.AccountId);
        Assert.Equal(TransactionType.Deposit, transaction.TransactionType);
    }
    
    [Fact]
    public void CreateWithdrawTransaction_WithInvalidAmount_ShouldThrowException()
    {
        var accountId = Guid.NewGuid();
        var money = Money.BRL(0M);
        const string description = "Saque do salario.";
        
        var ex = Assert.Throws<InvalidOperationException>(() 
            => Transaction.CreateWithdraw(accountId, money, description));
        
        Assert.Equal("O valor deve ser maior que zero.", ex.Message);
    }
    
    [Fact]
    public void CreateWithdrawTransaction_WithValidAmount_ShouldReturnSuccess()
    {
        var accountId = Guid.NewGuid();
        var money = Money.BRL(1500.0M);
        const string description = "Saque do salario.";
        
        var transaction = Transaction.CreateWithdraw(accountId, money, description);
        
        Assert.Equal(money, transaction.Amount);
        Assert.Equal(accountId, transaction.AccountId);
        Assert.Equal(TransactionType.Withdraw, transaction.TransactionType);
    }
    
    [Fact]
    public void CreatePaymentTransaction_WithInvalidAmount_ShouldThrowException()
    {
        var accountId = Guid.NewGuid();
        var money = Money.BRL(0M);
        const string description = "Pagamento da internet.";
        
        var ex = Assert.Throws<InvalidOperationException>(() 
            => Transaction.CreatePayment(accountId, money, description));
        
        Assert.Equal("O valor deve ser maior que zero.", ex.Message);
    }
    
    [Fact]
    public void CreatePaymentTransaction_WithValidAmount_ShouldReturnSuccess()
    {
        var accountId = Guid.NewGuid();
        var money = Money.BRL(1500.0M);
        const string description = "Pagamento da internet.";
        
        var transaction = Transaction.CreatePayment(accountId, money, description);
        
        Assert.Equal(money, transaction.Amount);
        Assert.Equal(accountId, transaction.AccountId);
        Assert.Equal(TransactionType.Payment, transaction.TransactionType);
    }
}