using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agendadorAulas.Model
{
    public class Aula
    {
        public int Id { get; set; }
        public string TipoAula { get; set; } = string.Empty;
        public DateTime DataHora { get; set; }
        public int CapacidadeMax { get; set; }

        public Aula() { }

        public Aula(string tipoAula, DateTime dataHora, int capacidadeMax)
        {
            TipoAula = tipoAula;
            DataHora = dataHora;
            CapacidadeMax = capacidadeMax;
        }

        public override string ToString() => $"[{Id}] {TipoAula} - {DataHora:dd/MM/yyyy HH:mm} (capacidade : {CapacidadeMax})";
    }
}