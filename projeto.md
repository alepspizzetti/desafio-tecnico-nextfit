# Desafio técnico Back-End Pleno

# 📝 Desafio:

Você foi contratado para desenvolver um módulo de **agendamento de aulas coletivas** para uma **academia**.

## 💻 Funcionalidades Esperadas:

- **Cadastro de alunos**, com nome e tipo de plano (`Mensal`, `Trimestral`, `Anual`).
- **Cadastro de aulas**, com:
    - Tipo da aula (ex: Cross, Funcional, Pilates)
    - Data/hora
    - Capacidade máxima de participantes
- **Agendamento de aluno em uma aula**, respeitando:
    - A capacidade da aula
    - O limite de agendamentos por plano no mês:
        - Mensal: até 12 aulas
        - Trimestral: até 20 aulas
        - Anual: até 30 aulas
- **Relatório simples por aluno**, retornando:
    - Total de aulas agendadas no mês
    - Lista dos tipos de aula que ele frequenta mais

## 🧪 Requisitos Técnicos:

- Projeto em C# com .NET 8 ou superior.

## ⚙️ Regras de Negócio:

- Um aluno **não pode** agendar mais aulas do que o plano permite.
- Uma aula **não pode ultrapassar** a capacidade máxima.
- Um aluno pode ser agendado em várias aulas no mês, desde que dentro do limite.

## ℹ️ Informações adicionais

- Pode ser uma Web Api ou um projeto Console.
- Lembre-se: este é um teste técnico. Apesar de ser simples, use todo o seu conhecimento e criatividade. Capriche na implementação — queremos ver do que você é capaz!
- Evite o uso excessivo de IA
- Este teste foi pensado para ser concluído em poucas horas, mas você terá um prazo de até 3 dias para entregar com tranquilidade.

## 📬 Forma de envio

- Subir no GitHub e enviar o link.