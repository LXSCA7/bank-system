using BankSystem.Domain.ValueObjects;

namespace BankSystem.Domain.Tests;

public class BirthDateTests
{
    [Fact]
    public void Create_WithValidDateTime_ShouldReturnSuccess()
    {
        var validDate = DateTime.Today.AddYears(-20).Date;
        
        var birthDate = BirthDate.Create(validDate);
        var verifyOperator = birthDate == validDate;
        var verifyOperator2 = validDate == birthDate;
        
        Assert.NotNull(birthDate);
        Assert.Equal(validDate, birthDate.Value);
        Assert.Equal(birthDate.Value, validDate);
        Assert.True(verifyOperator && verifyOperator2);
    }

    [Fact]
    public void Create_WithInvalidDateTime_ShouldReturnFailure()
    {
        var invalidDate = DateTime.Today;
        var ex = Assert.Throws<ArgumentException>(() => BirthDate.Create(invalidDate));
        Assert.Equal("Usuário deve ter pelo menos 18 anos.", ex.Message);
    }

    [Fact]
    public void Equality_BetweenDifferentDates_ShouldReturnFalse()
    {
        var date1 = BirthDate.Create(new DateTime(2000, 01, 01));
        var date2 = BirthDate.Create(new DateTime(2001, 01, 01));
        
        Assert.False(date1 == date2);
        Assert.False(date1.Equals(date2));
        Assert.NotEqual(date1, date2);
    }
    
    [Fact]
    public void Equality_BetweenDifferentDates_ShouldReturnTrue()
    {
        var date1 = BirthDate.Create(new DateTime(2000, 01, 01));
        var date2 = BirthDate.Create(new DateTime(2001, 01, 01));
        
        Assert.False(date1 == date2);
        Assert.False(date1.Equals(date2));
        Assert.NotEqual(date1, date2);
    }

}