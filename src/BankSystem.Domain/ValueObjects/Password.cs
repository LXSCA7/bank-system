using BankSystem.Domain.Exceptions;

namespace BankSystem.Domain.ValueObjects
{
    public record Password
    {
        public string Value { get; }

        private Password(string value) => Value = value;
        
        /// <summary>
        /// Cria e criptografa, usando BCrypt, uma senha a partir de um texto plano.
        /// </summary>
        /// <exception cref="WeakPasswordException">Lançada se a senha não atender aos critérios.</exception>
        /// <exception cref="ArgumentException">Lançada se o hash for nulo ou em branco.</exception>
        public static Password Create(string plainText)
        {
            Validate(plainText);
            var hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(plainText);
            return new(hashedPassword);
        }

        public static Password CreateFromHash(string hashedPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword))
                throw new ArgumentException("Hash inválido.");
            
            return new(hashedPassword);
        }

        private static void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new WeakPasswordException("Senha não pode ser vazia.");
            if (value.Length < 8)
                throw new WeakPasswordException("Senha deve conter ao menos 8 caracteres.");
            if (!value.Any(char.IsDigit))
                throw new WeakPasswordException("Senha deve conter ao menos um número.");
            if (!value.Any(char.IsLower))
                throw new WeakPasswordException("Senha deve conter ao menos uma letra minúscula.");
            if (!value.Any(char.IsUpper))
                throw new WeakPasswordException("Senha deve conter ao menos uma letra maiúscula.");
            if (value.Length > 64)
                throw new WeakPasswordException("Senha não pode exceder 64 caracteres.");
        }

        public bool VerifyPassword(string plainText) 
            => BCrypt.Net.BCrypt.EnhancedVerify(plainText, Value);
    }
}