using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using agendadorAulas.Helper;
using agendadorAulas.Repository;

namespace agendadorAulas.Services
{
    public class RelatorioService(AgendamentoRepository agendamentosRepository)
    {
        private readonly AgendamentoRepository _agendamentosRepository = agendamentosRepository;

        public List<string> QtdAgendamentoAlunoMes(int mes, int ano)
        {
            List<string> agendamentos = [];
            return agendamentos;
        }

        public int ValidaMes(string entradaMes)
        {
            if (!int.TryParse(entradaMes, out int mes) || mes < 1 || mes > 12)
                throw new ValidationException("Mês inválido");
            return mes;
        }

        public int ValidaAno(string entradaAno)
        {
            if (!int.TryParse(entradaAno, out int ano) || ano < 1800 || ano > 2099)
                throw new ValidationException("Ano inválido");
            return ano;
        }

        public List<FrequenciaAula> TiposAulaMaisFrequentadosPorAluno(int alunoId)
        {
            return _agendamentosRepository.ListarTopAulasPorAluno(alunoId);
        }
    }
}