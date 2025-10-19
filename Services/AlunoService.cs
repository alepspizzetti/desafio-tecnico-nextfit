using System.ComponentModel.DataAnnotations;
using System.Data;
using agendadorAulas.Model;
using agendadorAulas.Repository;

namespace agendadorAulas.Services
{
    public class AlunoService(AlunoRepository repository)
    {
        private readonly AlunoRepository _repository = repository;

        public void CadastrarAluno(Aluno aluno)
        {
            if (ValidaDuplicidadeAlunoNovo(aluno.Nome))
                _repository.SalvarAluno(aluno);
        }

        public bool ValidaDuplicidadeAlunoNovo(string nomeAluno)
        {
            if (_repository.ExisteAluno(nomeAluno))
                throw new ValidationException("Aluno já cadastrado");
            return true;
        }

        public static void ValidaNomeAluno(string nomeAluno)
        {
            if (string.IsNullOrWhiteSpace(nomeAluno))
                throw new ValidationException("O nome não pode estar vazio. Tente novamente.\n");

            if (nomeAluno.Any(char.IsDigit))
                throw new ValidationException("O nome não pode conter números. Tente novamente.\n");

            if (nomeAluno.Any(ch => !char.IsLetter(ch) && !char.IsWhiteSpace(ch)))
                throw new ValidationException("O nome não pode conter caracteres especiais. Tente novamente.\n");
        }

        public List<Aluno> ListarTodosAlunosDoSistema()
        {
            return _repository.ObterTodosAlunos()
                ?? throw new DataException("Não há alunos cadastrados");
        }

        public Aluno BuscarAluno(string aluno)
        {
            if (string.IsNullOrEmpty(aluno) || string.IsNullOrWhiteSpace(aluno))
                throw new ValidationException("Por favor !\nDigite um nome ou ID válido.");

            if (int.TryParse(aluno, out int id))
                return BuscarAlunoPorId(id);
            else
                return BuscarAlunoPorNome(aluno);
        }
        
        public Aluno BuscarAlunoPorId(int id)
        {
            return _repository.ObterAlunoPorId(id)
                ?? throw new DataException("Aluno não encontrado.");
        }

        public Aluno BuscarAlunoPorNome(string nome)
        {
            return _repository.ObterAlunoPorNome(nome)
                ?? throw new DataException("Aluno não encontrado.");
        }

        public void RemoverAluno(string aluno)
        {
            Aluno alunoExcluir = BuscarAluno(aluno);
            _repository.DeletarAluno(alunoExcluir);
        } 
    }
}