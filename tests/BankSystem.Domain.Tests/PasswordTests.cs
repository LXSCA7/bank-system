using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankSystem.Domain.Exceptions;
using BankSystem.Domain.ValueObjects;
using Xunit;

namespace BankSystem.Domain.Tests
{
    public class PasswordTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Create_WithNullOrEmptyPassword_ShoudThrowException(string userPassword)
        {
            var exception = Assert.Throws<WeakPasswordException>(() => Password.Create(userPassword));
            Assert.Equal("Senha fraca. Senha não pode ser vazia.", exception.Message);
        }

        [Fact]
        public void Create_WithPasswordLenghtLowerThanEight_ShoudThrowException()
        {
            var exception = Assert.Throws<WeakPasswordException>(() => Password.Create("1234567"));
            Assert.Equal("Senha fraca. Senha deve conter ao menos 8 caracteres.", exception.Message);
        }

        [Fact]
        public void Create_WithPasswordWithoutUpperCaseLetters_ShoudThrowException()
        {
            var exception = Assert.Throws<WeakPasswordException>(() => Password.Create("password123"));
            Assert.Equal("Senha fraca. Senha deve conter ao menos uma letra maiúscula.", exception.Message);
        }

        [Fact]
        public void Create_WithPasswordWithoutLowerCaseLetters_ShoudThrowException()
        {
            var exception = Assert.Throws<WeakPasswordException>(() => Password.Create("PASSWORD123"));
            Assert.Equal("Senha fraca. Senha deve conter ao menos uma letra minúscula.", exception.Message);
        }

        [Fact]
        public void Create_WithPasswordWithoutNumbers_ShoudThrowException()
        {
            var exception = Assert.Throws<WeakPasswordException>(() => Password.Create("Password"));
            Assert.Equal("Senha fraca. Senha deve conter ao menos um número.", exception.Message);
        }

        [Fact]
        public void Create_WithPasswordLengthGreaterThanMaxLength_ShoudThrowException()
        {
            string password = "J=`13Vf_q'8l0Ci>|4!wO193,,9uD3;7[Gy'gjJ%z;9]T_193daczxvvdcbs1349qweadsreuD3;7[Gy'gjJ%z;9]T^\\KxJ";
            var exception = Assert.Throws<WeakPasswordException>(() => Password.Create(password));
            Assert.Equal("Senha fraca. Senha não pode exceder 64 caracteres.", exception.Message);
        }

        [Theory]
        [InlineData("MyStr0ngP4ssw0rd!")]
        [InlineData("@UserStrongPassword123")]
        [InlineData("13tasdGTEAD9_BANK-SYSTEM_12aASd149DAsc")]
        public void Create_WithValidPassword_ShouldReturnPassword(string userStrongPassword)
        {
            Password password = Password.Create(userStrongPassword);
            bool check = password.VerifyPassword(userStrongPassword);
            Assert.True(check);
        }

        [Fact]
        public void Create_FromHashedPassword_ShouldReturnHashedPassword()
        {
            string hashed = "$2a$12$fo8g0ivuGbfmKBbuAYQs/.4kHGzEGWThSJabHTbsEMd7grZmWQFP.";
            Password password = Password.CreateFromHash(hashed);
            Assert.Equal(hashed, password.Value);
        }
    }
}