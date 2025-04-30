using BankSystem.Domain.Enums;
using BankSystem.Domain.ValueObjects;

namespace BankSystem.Domain.Tests
{
    public class DocumentTests
    {
        [Fact]
        public void Create_WithValidCpf_ShouldReturnCpf()
        {
            Document cpf = Document.Create("133.931.677-31");

            Assert.Equal(DocumentType.CPF, cpf.Type);
            Assert.Equal("13393167731", cpf.Value);
        }

        [Fact]
        public void Create_WithValidCnpj_ShouldReturnCnpj()
        {
            var cnpj = Document.Create("03.418.395/0001-35");

            Assert.Equal(DocumentType.CNPJ, cnpj.Type);
            Assert.Equal("03418395000135", cnpj.Value);
        }

        [Fact]
        public void Create_WithInvalidCpf_ShouldThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => Document.Create("123"));

            Assert.Equal("Tamanho do documento inválido.", exception.Message);
        }
    }
}