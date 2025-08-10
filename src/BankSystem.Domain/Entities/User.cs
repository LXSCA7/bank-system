using BankSystem.Domain.ValueObjects;

namespace BankSystem.Domain.Entities;

public class User : Entity
{
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public Guid UserType { get; private set; }
    public Guid CustomerId { get; private set; }

    private User(string email, string plainTextPassword, Guid userType, Guid customerId)
    {
        Email = Email.Create(email);
        Password = Password.Create(plainTextPassword);
        UserType = userType;
        CustomerId = customerId;
    }
    
    public static User Create(string email, string plainTextPassword, Guid userType, Guid customerId)
        => new(email,  plainTextPassword, userType, customerId);

    public void UpdateUserType(Guid userType)
    { 
        UserType = userType;
        Update();
    }

    public void UpdateEmail(string email)
    {
        Email = Email.Create(email);
        Update();
    }

    public void UpdatePassword(string password)
    {
        Password = Password.Create(password);
        Update();
    }
}
