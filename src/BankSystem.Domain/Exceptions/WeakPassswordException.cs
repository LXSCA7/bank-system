namespace BankSystem.Domain.Exceptions
{
    public class WeakPasswordException(string msg) 
        : Exception($"Senha invalida. {msg}");
}