using agendadorAulas.Data;
using agendadorAulas.Model;

namespace agendadorAulas.Repository
{
    public class AlunoRepository(AppDb db)
    {
        private readonly AppDb _db = db;

        public void SalvarAluno(Aluno aluno)
        {
            _db.Alunos.Add(aluno);
            _db.SaveChanges();
        }

        public List<Aluno> ObterTodosAlunos()
        {
            return _db.Alunos.ToList();
        }

        public Aluno? ObterAlunoPorId(int id)
        {
            return _db.Alunos.Find(id) ?? null;
        }

        public Aluno? ObterAlunoPorNome(string nome)
        {
            return _db.Alunos.FirstOrDefault(al => al.Nome == nome) ?? null;
        }

        public void DeletarAluno(Aluno aluno)
        {
            _db.Alunos.Remove(aluno);
            _db.SaveChanges();
        }

        //VALIDACAO: Um aluno não pode agendar mais aulas do que o plano permite.
        public int QtdAgendamentosNoMes(int alunoId, DateTime data)
        {
            return _db.Agendamentos.Where(ag => ag.AlunoId == alunoId
                && ag.DataHora.Month == data.Month
                && ag.DataHora.Year == data.Year
            ).Count();
        }

        public bool ExisteAluno(string nome)
        {
            return _db.Alunos.Any(aluno => aluno.Nome == nome);
        }
    }
}