# Agendador de Aulas

Sistema de agendamento de aulas desenvolvido em **C# (.NET 9)** com **Entity Framework Core** e **SQLite**.  
O projeto foi criado com foco em boas práticas de arquitetura, separando as camadas de **serviços**, **repositórios** e **acesso a dados**, aplicando validações e tratamento de exceções.

OBS: Projeto criado para demonstrar habilidades tecnicas seguindo o desafio imposto:
[Desafio Backend C# Pleno NextFit](https://mirror-path-0d6.notion.site/Desafio-t-cnico-Back-End-Pleno-1e041e6d421d8007987ae67786944e5b)

---

## Estrutura do Projeto

```
📁 agendadorAulas
├── Data/
│   ├── AppDb.cs
|   └── DesignTimeDbContextFactory.cs
├── Helper/
│   └── FrequenciaAula.cs
├── Model/
│   ├── Agendamento.cs
│   ├── Aluno.cs
│   ├── Aula.cs
│   └── Plano.cs
├── Repository/
│   ├── AgendamentoRepository.cs
│   ├── AlunoRepository.cs
│   └── AulaRepository.cs
├── Services/
│   ├── AgendamentoService.cs
│   ├── AlunoService.cs
│   ├── AulaService.cs
│   └── RelatorioService.cs
└── Program.cs
```

Cada camada tem uma responsabilidade clara:

- **Repository:** acesso direto ao banco de dados via `DbContext`  
- **Service:** lógica de negócio e validações  
- **Model:** modelagem de objetos
- **Helper:** classes auxiliares
- **Program.cs:** interface de interação via console

---

## Tecnologias Utilizadas

- **.NET 9.0**
- **C#**
- **Entity Framework Core**
- **SQLite**
- **Visual Studio Code**

---

## Funcionalidades

✅ Cadastro, listagem, busca e exclusão de alunos  
✅ Cadastro e gerenciamento de aulas  
✅ Agendamento de alunos em aulas com verificação de limite mensal  
✅ Relatórios com:
- Total de aulas agendadas por aluno no mês  
- Tipos de aula mais frequentados por aluno  

✅ Validações:
- Evita duplicidade de alunos e agendamentos  
- Impede Alunos excederem o limite do plano
- Valida capacidade máxima em aula
- Impede caracteres inválidos nos nomes  

---

## Banco de Dados

O sistema utiliza **SQLite** como banco local, com criação automática via `EnsureCreated()`.  
Relações configuradas no `AppDb`:

Chaves compostas e índices únicos foram aplicados para evitar duplicidade:

---

## Padrões e Boas Práticas Aplicadas

- **Separação de responsabilidades (Repository + Service + UI)**
- **Validações com exceções customizadas**
- **Tratamento de erros com mensagens amigáveis**
- **Consultas com `Include()` para joins automáticos via EF Core**

---

## Possíveis Melhorias Futuras

- Criar uma interface web com ASP.NET MVC 
- Adicionar autenticação simples para controle de acesso  
- Criar exportação de relatórios em PDF

---

## Autor

**Alessandro Pizzetti**  
💼 [LinkedIn](https://www.linkedin.com/in/alepspizzetti)  

---

## 📜 Licença

Este projeto é de uso livre para fins de estudo e demonstração de habilidades técnicas.
