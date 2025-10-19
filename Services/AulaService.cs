using System.ComponentModel.DataAnnotations;
using System.Data;
using agendadorAulas.Data;
using agendadorAulas.Model;
using agendadorAulas.Repository;
using Microsoft.EntityFrameworkCore;

namespace agendadorAulas.Services
{
  public class AulaService(AulaRepository repository)
  {
    private readonly AulaRepository _repository = repository;

    public void CadastrarAula(Aula aula)
    {
      _repository.SalvarAula(aula);
    }

    public void ValidaTipoAula(string tipoAula)
    {
      if (string.IsNullOrWhiteSpace(tipoAula))
        throw new ValidationException("A descrição do tipo de aula não pode estar vazio. Tente novamente.\n");

      if (tipoAula.Any(char.IsDigit))
        throw new ValidationException("A descrição do tipo de aula não pode conter números. Tente novamente.\n");

      if (tipoAula.Any(ch => !char.IsLetter(ch) && !char.IsWhiteSpace(ch)))
        throw new ValidationException("A descrição do tipo de aula não pode conter caracteres especiais. Tente novamente.\n");

      if (_repository.ExisteAula(tipoAula))
        throw new DataException("Aula já cadastrada");
    } 

    public static DateTime ValidaDataAula(string entradaData)
    {
      if (!DateTime.TryParseExact(
        entradaData,
        "dd/MM/yyyy HH:mm",
        System.Globalization.CultureInfo.InvariantCulture,
        System.Globalization.DateTimeStyles.None,
        out DateTime data
      ))
        throw new FormatException("Formato da data é inválido, Tente novamente.");
      return data;
    }

    public static void ValidaCapacidade(string capacidadeMax)
    {
      if (!int.TryParse(capacidadeMax, out int capacidade))
        throw new FormatException("Valor incorreto, digite somente números.");
    }

    public List<Aula> ListarTodasAulasDoSistema()
    {
      return _repository.ObterTodasAulas()
        ?? throw new DataException("Não há alunos cadastrados");
    }

    public Aula BuscarAula(string aula)
    {
      if (string.IsNullOrEmpty(aula) || string.IsNullOrWhiteSpace(aula))
        throw new ValidationException("Por favor !\nDigite um tipo ou ID válido.");

      if (int.TryParse(aula, out int id))
        return BuscarAulaPorId(id);
      else
        return BuscarAulaPorTipo(aula);
    }

    public Aula BuscarAulaPorId(int id)
    {
      return _repository.ObterAulaPorId(id)
          ?? throw new DataException("Aula não encontrada.");
    }

    public Aula BuscarAulaPorTipo(string tipo)
    {
      return _repository.ObterAulaPorTipo(tipo)
          ?? throw new DataException("Aula não encontrada.");
    }

    public void RemoverAula(string aula)
    {
      Aula aulaExcluir = BuscarAula(aula);
      _repository.DeletarAula(aulaExcluir);
    }
  }
}