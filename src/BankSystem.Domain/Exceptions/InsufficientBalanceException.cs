using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankSystem.Domain.ValueObjects;

namespace BankSystem.Domain.Exceptions
{
    public class InsufficientBalanceException(Money balance) 
        : Exception($"Saldo insuficente. Saldo atual: {balance}");
}