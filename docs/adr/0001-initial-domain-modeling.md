# ADR 0001: Modelagem inicial de Entitades e Value Objects, usando classes e records em C#.

---

**Titulo: _Modelagem inicial de Entitades e Value Objects, usando classes e records em C#._**

**Status: _Obsoleta, consulte [ADR-0002](./0002-refactoring-domain-models.md)_**

## Contexto:

Estou iniciando o desenvolvimento do sistema bancário e preciso definir a abordagem fundamental para a modelagem de domínio. O objetivo é criar um modelo que seja expressivo, seguro, coeso e que reflita as regras de negócio do domínio bancário. A escolha das ferramentas e padrões de design para representar os conceitos-chave (como contas, transações, valores monetários e dados de usuário) é crucial para a integridade e manutenibilidade do sistema.

## Decisao
Decidi adotar uma arquitetura de domínio rica, utilizando os tipos class e record do C# para modelar Entidades e Value Objects, respectivamente.

