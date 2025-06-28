using BankSystem.Domain.Entities;
using BankSystem.Domain.Exceptions;
using BankSystem.Domain.ValueObjects;

namespace BankSystem.Domain.Tests;

public class AccountTests
{
    [Theory]
    [InlineData("Account number can't be null or empty.", "")]
    [InlineData("Account number lenght needs to be <= 10.", "12345678901")]
    [InlineData("Account number can only have digits.", "abc")]
    [InlineData("Account number can only have digits.", "123abc")]
    public void CreateAccount_WithInvalidAccountNumber_ShouldThrowsException(string errorMessage, string accountNumber)
    {
        var ex = Assert.Throws<ArgumentException>(() => Account.Create(accountNumber,  Guid.NewGuid()));
        
        Assert.Equal("accountNumber", ex.ParamName);
        Assert.Equal($"Invalid account number: {errorMessage} (Parameter 'accountNumber')", ex.Message);
    }
    
    [Fact]
    public void CreateAccount_WithInvalidCostumerId_ShouldThrowsException()
    {
        var ex = Assert.Throws<ArgumentException>(() => Account.Create("0000000001",  Guid.NewGuid()));
        
        Assert.Equal("accountNumber", ex.ParamName);
        Assert.Equal("Invalid account number (Parameter 'accountNumber')", ex.Message);
    }

    [Fact]
    public void CreateAccount_WithValidParameters_ShouldReturnSuccess()
    {
        const int maxLength = 10;
        const string accountNumber = "123456789";
        var expectedAccountNumber = accountNumber.PadLeft(maxLength, '0');
        var costumerId = Guid.NewGuid();
        
        var account = Account.Create(accountNumber, costumerId);
        
        Assert.Equal(expectedAccountNumber, account.AccountNumber);
        Assert.Equal(costumerId, account.CostumerId);
        Assert.Equal(0.0M, account.Balance.Amount);
    }

    [Fact]
    public void Withdraw_WithDifferentCurrency_ShouldThrowException()
    {
        var acc = Account.Create("1",  Guid.NewGuid());
        var money = Money.USD(150.00M);
        
        var ex = Assert.Throws<InvalidOperationException>(() => acc.Withdraw(money));
        
        Assert.Equal("Moeda do saque diferente da conta.", ex.Message);
        Assert.Equal(0, acc.Balance.Amount);
    }
    
    [Fact]
    public void Withdraw_WithInsufficientBalance_ShouldThrowException()
    {
        var acc = Account.Create("1",  Guid.NewGuid());
        var money = Money.BRL(150.00M);
        
        var ex = Assert.Throws<InsufficientBalanceException>(() => acc.Withdraw(money));
        
        Assert.Equal($"Saldo insuficente. Saldo atual: {Money.BRL(0)}", ex.Message);
        Assert.Equal(0, acc.Balance.Amount);
    }
    
    [Fact]
    public void Deposit_WithDifferentCurrency_ShouldThrowException()
    {
        var acc = Account.Create("1",  Guid.NewGuid());
        var money = Money.USD(150.00M);
        
        var ex = Assert.Throws<InvalidOperationException>(() => acc.Deposit(money));
        
        Assert.Equal("Moeda do depósito diferente da conta.", ex.Message);
        Assert.Equal(0, acc.Balance.Amount);
    }
    
    [Theory]
    [InlineData(50.00)]
    [InlineData(250.591)]
    [InlineData(140.41)]
    public void Deposit_WithSameCurrency_ShouldReturnSuccess(decimal amount)
    {
        var acc = Account.Create("1",  Guid.NewGuid());
        var money = Money.BRL(amount);

        acc.Deposit(money);
        
        Assert.Equal(money, acc.Balance);
    }
    
    [Fact]
    public void Withdraw_WithSameCurrency_ShouldReturnSuccess()
    {
        var acc = Account.Create("1",  Guid.NewGuid());
        var money = Money.BRL(150.00M);
        var moneyToWithdraw = Money.BRL(100.0M);
        
        acc.Deposit(money);
        acc.Withdraw(moneyToWithdraw);
        
        Assert.Equal(money - moneyToWithdraw, acc.Balance);
    }
}