using agendadorAulas.Data;
using agendadorAulas.Helper;
using agendadorAulas.Model;
using Microsoft.EntityFrameworkCore;

namespace agendadorAulas.Repository
{
    public class AgendamentoRepository(AppDb db)
    {
        private readonly AppDb _db = db;

        public void SalvarAgendamento(Agendamento agendamento)
        {
            _db.Agendamentos.Add(agendamento);
            _db.SaveChanges();
        }

        public List<Agendamento> ObterTodosAgendamentos()
        {
            return _db.Agendamentos.Include(ag => ag.Aula)
                .Include(ag => ag.Aluno)
                .OrderBy(ag => ag.Aula.TipoAula)
                .ThenBy(ag => ag.Aluno.Nome)
                .ToList();
        }

        public Agendamento? ObterAgendamentoPorId(int id)
        {
            return _db.Agendamentos.Find(id) ?? null;
        }

        public List<Agendamento> ObterAgendamentosPorAluno(int alunoId)
        {
            return _db.Agendamentos.Where(ag => ag.AlunoId == alunoId).ToList();
        }

        public List<Agendamento> ObterAgendamentosPorAula(int aulaId)
        {
            return _db.Agendamentos.Where(ag => ag.AulaId == aulaId).ToList();
        }

        public Agendamento? ObterAgendamento(int alunoId, int aulaId)
        {
            return _db.Agendamentos.FirstOrDefault(ag => ag.AlunoId == alunoId
                && ag.AulaId == aulaId) ?? null;
        }

        public void DeletarAgendamento(Agendamento agendamento)
        {
            _db.Agendamentos.Remove(agendamento);
            _db.SaveChanges();
        }

        public bool ExisteAgendamento(int alunoId, int aulaId, DateTime data)
        {
            return _db.Agendamentos.Any(ag => ag.AlunoId == alunoId
                && ag.AulaId == aulaId
                && ag.DataHora == data);
        }

        public int QtdAgendamentosPorAlunoMes(int alunoId, int mes, int ano)
        {
            return _db.Agendamentos.Count(ag => ag.AlunoId == alunoId
                && ag.DataHora.Month == mes
                && ag.DataHora.Year == ano);
        }

        public List<FrequenciaAula> ListarTopAulasPorAluno(int alunoId)
        {
            return _db.Agendamentos.Where(ag => ag.AlunoId == alunoId)
                .GroupBy(ag => ag.Aula.TipoAula)
                .Select(ag => new FrequenciaAula{TipoAula = ag.Key, QtdAgendamento = ag.Count()})
                .OrderByDescending(ord => ord.QtdAgendamento)
                .ToList();
        }
    }
}