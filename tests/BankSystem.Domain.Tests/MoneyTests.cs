using BankSystem.Domain.ValueObjects;

namespace BankSystem.Domain.Tests;

public class MoneyTests
{
    [Fact]
    public void Create_WithValidValues_ShouldInitializeProperties()
    {
        var money = Money.BRL(1000.50M);

        Assert.Equal(1000.50M,  money.Amount);
        Assert.Equal("BRL", money.Currency);
    }

    [Theory]
    [InlineData(-100)]
    [InlineData(-0.01)]
    public void Create_WithNegativeAmount_ShouldThrowException(decimal amount) 
    {
        var exception = Assert.Throws<ArgumentException>(() => Money.BRL(amount));
        Assert.Equal("O valor não pode ser negativo. (Parameter 'amount')", exception.Message);
    }
    
    [Theory]
    [InlineData("EUR")]
    [InlineData("eur")]
    public void Create_WithValidCurrency_ShouldInitializeProperties(string currency) 
    {
        var money = Money.Create(500, currency);

        Assert.Equal(500, money.Amount);
        Assert.Equal("EUR", money.Currency);
    }

    [Fact]
    public void Create_WithInvalidCurrency_ShouldThrowException()
    {
        var exception = Assert.Throws<ArgumentException>(() => Money.Create(150.0M, "Real"));
        Assert.Equal("A moeda deve conter no máximo 3 caracteres. Exemplo: \"BRL\"."
                        + " (Parameter 'currency')", exception.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_WithNullOrEmptyCurrency_ShouldThrowException(string currency) {
        var exception = Assert.Throws<ArgumentException>(() => Money.Create(150.0M, currency));
        Assert.Equal("A moeda não pode ser vazia. (Parameter 'currency')", exception.Message);
    }

    [Fact]
    public void Add_WithSameCurrency_ShouldReturnSum() 
    {
        var money1 = Money.BRL(100.0M);
        var money2 = Money.BRL(300.0M);

        var result = money1 + money2;

        Assert.Equal(400, result.Amount);
        Assert.Equal("BRL", result.Currency);
    }

    [Fact]
    public void Add_WithDifferentCurrencies_ShoudThrowException()
    {
        var brl = Money.BRL(500);
        var usd = Money.USD(100);

        var exception = Assert.Throws<InvalidOperationException>(() => brl + usd);
        
        Assert.Equal("Não é possivel somar moedas diferentes.", exception.Message);
    }

    [Fact]
    public void Subtract_WithSameCurrency_ShouldReturnDifference()
    {
        var money1 = Money.BRL(1000);
        var money2 = Money.BRL(350);

        var result = money1 - money2;

        Assert.Equal(650, result.Amount);
        Assert.Equal("BRL", result.Currency);
    }

    [Fact]
    public void Subtract_WithDifferentCurrencies_ShoudThrowException()
    {
        var brl = Money.BRL(600);
        var usd = Money.USD(100);

        var exception = Assert.Throws<InvalidOperationException>(() => brl - usd);
        
        Assert.Equal("Não é possivel subtrair moedas diferentes.", exception.Message);
    }


    [Fact]
    public void Subtract_ResultingInNegativeAmount_ShouldThrowException()
    {
        var money1 = Money.BRL(100);
        var money2 = Money.BRL(200);

        Assert.Throws<ArgumentException>(() => money1 - money2);
    }

    [Fact]
    public void LessThanOperator_WithSameCurrency_ShouldReturnTrue()
    {
        var smaller = Money.BRL(10);
        var larger = Money.BRL(11);

        Assert.True(smaller < larger);
    }

    [Fact]
    public void LessThanOperator_WithDifferentCurrency_ShouldReturnFalse()
    {
        var smaller = Money.BRL(10);
        var larger = Money.USD(11);

        Assert.False(smaller < larger);
    }

    [Fact]
    public void GreaterThanOperator_WithSameCurrency_ShouldReturnTrue()
    {
        var smaller = Money.BRL(10);
        var larger = Money.BRL(11);

        Assert.True(larger > smaller);
    }

    [Fact]
    public void GreaterThanOperator_WithDifferentCurrency_ShouldReturnFalse()
    {
        var smaller = Money.BRL(10);
        var larger = Money.USD(11);

        Assert.False(larger > smaller);
    }

    [Fact]
    public void ToString_ShouldRerturnFormattedString() {
        var money = Money.BRL(150.50M);
        Assert.Equal("150.50 BRL", money.ToString());
    }
}