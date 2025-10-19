using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace agendadorAulas.Model
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string TipoPlano { get; set; }
        public int QtdAulasPlano { get; set; }

        public Aluno()
        { 
            Nome = string.Empty;
            TipoPlano = string.Empty;
            QtdAulasPlano = int.MaxValue;
        }

        public Aluno(string nome, string tipoPlano, int qtdAulasPlano)
        {
            this.Nome = nome;
            this.TipoPlano = tipoPlano;
            this.QtdAulasPlano = qtdAulasPlano;
        }

        public Aluno(int id, string nome, string tipoPlano, int qtdAulasPlano, int aulasAgendadas)
        {
            this.Id = id;
            this.Nome = nome;
            this.TipoPlano = tipoPlano;
            this.QtdAulasPlano = qtdAulasPlano;
        }

        public override string ToString()
        {
            return $"[ID: {Id}] - {Nome}\n--> Plano: {TipoPlano} \n--> Agendamentos por mês: {QtdAulasPlano}";
        }
    }
}