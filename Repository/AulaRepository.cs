using agendadorAulas.Data;
using agendadorAulas.Model;

namespace agendadorAulas.Repository
{
    public class AulaRepository(AppDb db)
    {
        private readonly AppDb _db = db;

        public void SalvarAula(Aula aula)
        {
            _db.Aulas.Add(aula);
            _db.SaveChanges();
        }

        public List<Aula> ObterTodasAulas()
        {
            return _db.Aulas.ToList();
        }

        public Aula? ObterAulaPorId(int id)
        {
            return _db.Aulas.Find(id) ?? null;
        }

        public Aula? ObterAulaPorTipo(string tipo)
        {
            return _db.Aulas.FirstOrDefault(al => al.TipoAula == tipo) ?? null;
        }

        public void DeletarAula(Aula aula)
        {
            _db.Aulas.Remove(aula);
            _db.SaveChanges();
        }

        public bool ExisteAula(string tipo)
        {
            return _db.Aulas.Any(al => al.TipoAula == tipo);
        }

        //VALIDACAO: Uma aula não pode ultrapassar a capacidade máxima.
        public int QtdAlunosAula(int aulaId)
        {
            return _db.Agendamentos.Count(ag => ag.AulaId == aulaId);
        }
    }
}