using System.Text.RegularExpressions;
using BankSystem.Domain.Enums;

namespace BankSystem.Domain.ValueObjects
{
    public record Document
    {
        public string Value;
        public DocumentType Type;
        private Document(string value, DocumentType type)
        {
            Value = value;
            Type = type;
        }

        public static Document Create(string rawValue)
        {
            if (string.IsNullOrEmpty(rawValue))
                throw new ArgumentException("Documento não pode estar em branco.");
            var cleanedValue = CleanDocument(rawValue);
            var type = DetermineDocumentType(cleanedValue);

            return type switch {
                DocumentType.CPF when IsValidCpf(cleanedValue) => new Document(cleanedValue, type),
                DocumentType.CNPJ when IsValidCnpj(cleanedValue) => new Document(cleanedValue, type),
                _ => throw new ArgumentException("Documento inválido. São aceitos apenas CPFs e CNPJs.")
            };
        }

        private static string CleanDocument(string value)
            => Regex.Replace(value, @"[^\d]", "");

        private static DocumentType DetermineDocumentType(string value)
            => value.Length switch {
                11 => DocumentType.CPF,
                14 => DocumentType.CNPJ,
                _ => throw new ArgumentException("Tamanho do documento inválido.")
            };

        private static bool IsValidCpf(string value)
        {
            // falta implementar o algoritmo pra verificar.
            return value.Length == 11;
        }

         private static bool IsValidCnpj(string value)
        {
            // falta implementar o algoritmo pra verificar.
            return value.Length == 14;
        }

        public static string FormatDocument(string value, DocumentType type) 
            => type switch
            {
                DocumentType.CPF => Convert.ToUInt64(value).ToString(@"000\.000\.000\-00"),
                DocumentType.CNPJ => Convert.ToUInt64(value).ToString(@"00\.000\.000\/0000\-00"),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };

        public override string ToString() => FormatDocument(Value, Type);
   }
}