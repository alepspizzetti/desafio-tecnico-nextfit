using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using agendadorAulas.Data;
using agendadorAulas.Model;
using agendadorAulas.Repository;
using Microsoft.EntityFrameworkCore;

namespace agendadorAulas.Services
{
    public class AgendamentoService(AgendamentoRepository agendamentosRepository, AulaRepository aulaRepository, AlunoRepository alunoRepository)
    {
        private readonly AgendamentoRepository _agendamentosRepository = agendamentosRepository;
        private readonly AulaRepository _aulaRepository = aulaRepository;
        private readonly AlunoRepository _alunoRepository = alunoRepository;

        public void CadastrarAgendamento(Aluno aluno, Aula aula)
        {
            ValidaLimiteAgendamento(aluno.Id, aluno.QtdAulasPlano, aula.DataHora);
            ValidaCapacidadeAula(aula.Id, aula.CapacidadeMax);
            Agendamento agendamento = new(aula.DataHora, aluno, aula);
            _agendamentosRepository.SalvarAgendamento(agendamento);
        }

        public void ValidaLimiteAgendamento(int alunoId, int qtdAulasPlanoAluno, DateTime data)
        {
            int qtdAgendamentos = _alunoRepository.QtdAgendamentosNoMes(alunoId, data);
            if ((qtdAgendamentos + 1) > qtdAulasPlanoAluno)
                throw new InvalidOperationException("Limite de agendamentos no mês excedido\n>>> Faça um upgrade de plano para agendar mais aulas! <<<");
        }
        
        public void ValidaCapacidadeAula(int aulaId, int capacidadeMax)
        {
            int capacidadeAula = _aulaRepository.QtdAlunosAula(aulaId);
            if ((capacidadeAula + 1) > capacidadeMax)
                throw new InvalidOperationException("Esta aula já está cheia");
        }

        public List<Agendamento> ListarAgendamentos()
        {
            return _agendamentosRepository.ObterTodosAgendamentos();
        }

        public List<Agendamento> ListarAgendamentosPorAluno(int alunoId)
        {
            return _agendamentosRepository.ObterAgendamentosPorAluno(alunoId);
        }

        public List<Agendamento> ListarAgendamentosPorAula(int aulaId)
        {
            return _agendamentosRepository.ObterAgendamentosPorAula(aulaId);
        }

        public void RemoverAgendamento(int alunoId, int aulaId)
        {
            Agendamento agExcluir = _agendamentosRepository.ObterAgendamento(alunoId, aulaId)!;
            _agendamentosRepository.DeletarAgendamento(agExcluir);
        }

        public int TotalAulasAgendasPorAlunoNoMes(int alunoId, int mes, int ano)
        {
            return _agendamentosRepository.QtdAgendamentosPorAlunoMes(alunoId, mes, ano);
        }
    }
}