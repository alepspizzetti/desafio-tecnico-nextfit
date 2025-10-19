using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace agendadorAulas.Model
{
    public class Agendamento
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int AulaId { get; set; }
        public DateTime DataHora { get; set; }
        public Aluno Aluno { get; set; } = new();
        public Aula Aula { get; set; } = new();

        public Agendamento() {}

        public Agendamento(DateTime dataHora, Aluno aluno, Aula aula)
        {
            this.AlunoId = aluno.Id;
            this.AulaId = aula.Id;
            this.DataHora = dataHora;
            this.Aluno = aluno;
            this.Aula = aula;
        }

        public override string ToString() => $"[ID: {Id}] Aula: {Aula.TipoAula} - Aluno: {Aluno.Nome} -> {DataHora:dd/MM/yyyy HH:mm}";
    }
}