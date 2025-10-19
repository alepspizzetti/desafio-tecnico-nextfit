using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using agendadorAulas.Model;
using agendadorAulas.Services;
using agendadorAulas.Data;
using System.Data;
using System.ComponentModel.DataAnnotations;
using agendadorAulas.Helper;
using agendadorAulas.Repository;

namespace agendadorAulas
{
    class Program
    {
        private static AlunoService _alunoService = null!;
        private static AulaService _aulaService = null!;
        private static AgendamentoService _agendamentoService = null!;
        private static RelatorioService _relatorioService = null!;

        #region Main
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<AppDb>()
                .UseSqlite("Data Source=agendador.db")
                .Options;

            using var db = new AppDb(options);
            db.Database.EnsureCreated();

            var alunoRepository = new AlunoRepository(db);
            _alunoService = new AlunoService(alunoRepository);

            var aulaRepository = new AulaRepository(db);
            _aulaService = new AulaService(aulaRepository);

            var agendamentoRepository = new AgendamentoRepository(db);
            _agendamentoService = new AgendamentoService(agendamentoRepository, aulaRepository, alunoRepository);

            var relatorioRepository = new AgendamentoRepository(db);
            _relatorioService = new RelatorioService(relatorioRepository);

            bool sair = false;
            while (!sair)
            {
                Console.Clear();
                Console.WriteLine("[=== SISTEMA DE AGENDAMENTO DE AULAS ===]");
                Console.WriteLine("1 -> Agendamentos");
                Console.WriteLine("2 -> Alunos");
                Console.WriteLine("3 -> Aulas");
                Console.WriteLine("4 -> Relatórios");
                Console.WriteLine("0 -> Fechar");
                Console.Write("\nDigite o número correspondente a opção Desejada: ");

                string opcao = Console.ReadLine() ?? string.Empty;

                switch (opcao)
                {
                    case "1":
                        MenuAgendamentos();
                        break;
                    case "2":
                        MenuAlunos();
                        break;
                    case "3":
                        MenuAulas();
                        break;
                    case "4":
                        MenuRelatorios();
                        break;
                    case "0":
                        sair = true;
                        break;
                    default:
                        Console.Write("\n      >>> Opção inválida <<<\nDigite novamente o número correspondente a opção Desejada: ");
                        Console.ReadKey();
                        break;
                }
            }
        }
        #endregion

        #region Alunos
        private static void MenuAlunos()
        {
            bool voltar = false;
            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("[=== MENU DE ALUNOS ===]");
                Console.WriteLine("1 -> Novo Aluno");
                Console.WriteLine("2 -> Listar Alunos");
                Console.WriteLine("3 -> Buscar Aluno");
                Console.WriteLine("4 -> Remover Aluno");
                Console.WriteLine("0 -> Menu Anterior");
                Console.Write("\nDigite o número correspondente a opção Desejada: ");

                string opcao = Console.ReadLine() ?? string.Empty;

                switch (opcao)
                {
                    case "1":
                        CadastrarAluno();
                        break;
                    case "2":
                        ListarAlunos();
                        break;
                    case "3":
                        BuscarAluno();
                        break;
                    case "4":
                        RemoverAluno();
                        break;
                    case "0":
                        voltar = true;
                        break;
                    default:
                        Console.WriteLine(">>> Opção inválida <<< \n Digite novamente o número correspondente a opção Desejada: ");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void CadastrarAluno()
        {
            Console.Clear();
            Console.WriteLine("[=== NOVO ALUNO ===]");

            string nome;
            while (true)
            {
                Console.Write("> Nome: ");
                nome = Console.ReadLine()!;
                try
                {
                    AlunoService.ValidaNomeAluno(nome);
                    break;
                }
                catch (ValidationException vex)
                {
                    Console.WriteLine(vex.Message);
                }
            }


            Console.WriteLine("> Tipo de Plano:");
            Console.WriteLine("1 -> Mensal(12 Aulas)");
            Console.WriteLine("2 -> Trimestral(20 Aulas)");
            Console.WriteLine("3 -> Anual(30 Aulas)");
            Console.Write("Escolha o plano: ");

            string tipoPlano;
            int qtdAulasPlano;
            switch (Console.ReadLine() ?? string.Empty)
            {
                case "1":
                    tipoPlano = "Mensal";
                    qtdAulasPlano = Plano.QtdAulaPorPlano(tipoPlano);
                    break;
                case "2":
                    tipoPlano = "Trimestral";
                    qtdAulasPlano = Plano.QtdAulaPorPlano(tipoPlano);
                    break;
                case "3":
                    tipoPlano = "Anual";
                    qtdAulasPlano = Plano.QtdAulaPorPlano(tipoPlano);
                    break;
                default:
                    Console.Write("\n>>> Opção inválida <<<\nDigite o número correspondente a opção desejada: ");
                    Console.ReadKey();
                    return;
            }

            var aluno = new Aluno(nome, tipoPlano, qtdAulasPlano);
            try
            {
                _alunoService.CadastrarAluno(aluno);
                Console.WriteLine("Aluno cadastrado com sucesso!");
            }
            catch (ValidationException vex)
            {
                Console.WriteLine(vex.Message);
            }
            
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private static void ListarAlunos()
        {
            Console.Clear();
            Console.WriteLine("[=== LISTA DE ALUNOS ===]");
            
            List<Aluno> alunos = [];
            try
            {
                alunos = _alunoService.ListarTodosAlunosDoSistema();
            }
            catch (DataException dex)
            {
                Console.WriteLine(dex.Message);
            }

            if (alunos.Count > 0)
            {
                foreach (var aluno in alunos)
                    Console.WriteLine(aluno);

            }
            else
                Console.WriteLine("Nenhum aluno cadastrado.");

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private static void BuscarAluno()
        {
            Console.Clear();
            Console.WriteLine("[=== BUSCAR ALUNO ===]");
            while (true)
            {
                Console.Write("Digite o ID ou o Nome do aluno: ");
                string entradaAluno = Console.ReadLine() ?? string.Empty;
                try
                {
                    var aluno = _alunoService.BuscarAluno(entradaAluno);
                    Console.WriteLine(aluno);
                    break;
                }
                catch (DataException dex)
                {
                    Console.WriteLine(dex.Message);
                }
                catch (ValidationException vex)
                {
                    Console.WriteLine(vex.Message);
                }   
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private static void RemoverAluno()
        {
            Console.Clear();
            Console.WriteLine("[=== REMOVER ALUNO ===]");

            while (true)
            {
                Console.Write("Digite o ID ou o Nome do aluno: ");
                string entradaAluno = Console.ReadLine() ?? string.Empty;
                try
                {
                    _alunoService.RemoverAluno(entradaAluno);
                    Console.WriteLine("Aluno excluído com sucesso!");
                    break;
                }
                catch (DataException dex)
                {
                    Console.WriteLine(dex.Message);
                }
                catch (ValidationException vex)
                {
                    Console.WriteLine(vex.Message);
                }
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        #endregion

        #region Aulas
        private static void MenuAulas()
        {
            bool voltar = false;
            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("[=== MENU DE AULAS ===]");
                Console.WriteLine("1 -> Nova Aula");
                Console.WriteLine("2 -> Listar Aulas");
                Console.WriteLine("3 -> Buscar Aula");
                Console.WriteLine("4 -> Remover Aula");
                Console.WriteLine("0 -> Menu Anterior");
                Console.Write("\nDigite o número correspondente a opção Desejada: ");

                string opcao = Console.ReadLine() ?? string.Empty;

                switch (opcao)
                {
                    case "1":
                        CadastrarAula();
                        break;
                    case "2":
                        ListarAulas();
                        break;
                    case "3":
                        BuscarAula();
                        break;
                    case "4":
                        RemoverAula();
                        break;
                    case "0":
                        voltar = true;
                        break;
                    default:
                        Console.WriteLine(">>> Opção inválida <<< \n Digite novamente o número correspondente a opção Desejada: ");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void CadastrarAula()
        {
            Console.Clear();
            Console.WriteLine("[=== NOVA AULA ===]");

            string tipo;
            while (true)
            {
                Console.Write("> Tipo de aula: ");
                tipo = Console.ReadLine()!;
                try
                {
                    _aulaService.ValidaTipoAula(tipo);
                    break;
                }
                catch (ValidationException vex)
                {
                    Console.WriteLine(vex.Message);
                }
                catch (DataException dex)
                {
                    Console.WriteLine(dex.Message);
                }
            }

            DateTime dataAula = DateTime.Now;
            while (true)
            {
                Console.Write("> Data (dd/MM/aaaa HH:mm): ");
                var entradaData = Console.ReadLine() ?? DateTime.Now.ToString();
                try
                {
                    dataAula = AulaService.ValidaDataAula(entradaData);
                    break;
                }
                catch (FormatException fex)
                {
                    Console.WriteLine(fex.Message);
                }
                
            }

            int capacidadeMax;
            while (true)
            {
                Console.Write("Capacidade Máxima: ");
                string capacidade = Console.ReadLine()!;
                try
                {
                    AulaService.ValidaCapacidade(capacidade);
                    capacidadeMax = Convert.ToInt32(capacidade);
                    break;
                }
                catch (FormatException fex)
                {
                    Console.WriteLine(fex.Message);
                }
            }

            Aula aula = new(tipo, dataAula, capacidadeMax);
            try
            {
                _aulaService.CadastrarAula(aula);
                Console.WriteLine("Cadastrado com sucesso!");    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private static void ListarAulas()
        {
            Console.Clear();
            Console.WriteLine("[=== LISTA DE AULAS ===]");

            List<Aula> aulas = [];
            try
            {
                aulas = _aulaService.ListarTodasAulasDoSistema();
            }
            catch (DataException dex)
            {
                Console.WriteLine(dex.Message);
            }
            
            foreach (var aula in aulas)
            {
                Console.WriteLine(aula);
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private static void BuscarAula()
        {
            Console.Clear();
            Console.WriteLine("[=== BUSCAR AULA ===]");

            while (true)
            {
                Console.Write("Digite o nome da aula: ");
                string entradaAula = Console.ReadLine() ?? string.Empty;
                try
                {
                    var aula = _aulaService.BuscarAula(entradaAula);
                    Console.WriteLine(aula);
                    break;
                }
                catch (ValidationException vex)
                {
                    Console.WriteLine(vex.Message);
                }
                catch (DataException dex)
                {
                    Console.WriteLine(dex.Message);
                }   
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private static void RemoverAula()
        {
            Console.Clear();
            Console.WriteLine("[=== REMOVER AULA ===]");

            while (true)
            {
                Console.Write("Digite o ID ou o Nome da aula: ");
                string entradaAula = Console.ReadLine() ?? string.Empty;
                try
                {
                    _aulaService.RemoverAula(entradaAula);
                    Console.WriteLine("Aula excluída com sucesso!");
                    break;
                }
                catch (DataException dex)
                {
                    Console.WriteLine(dex.Message);
                }
                catch (ValidationException vex)
                {
                    Console.WriteLine(vex.Message);
                }
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        #endregion

        #region Agendamentos
        private static void MenuAgendamentos()
        {
            bool voltar = false;
            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("[=== MENU DE AGENDAMENTOS ===]");
                Console.WriteLine("1 -> Agendar Aluno em Aula");
                Console.WriteLine("2 -> Listar Agendamentos");
                Console.WriteLine("3 -> Remover Agendamento");
                Console.WriteLine("0 -> Menu Anterior");
                Console.Write("\nDigite o número correspondente a opção Desejada: ");

                string opcao = Console.ReadLine() ?? string.Empty;

                switch (opcao)
                {
                    case "1":
                        AgendarAula();
                        break;
                    case "2":
                        ListarAgendamentos();
                        break;
                    case "3":
                        RemoverAgendamento();
                        break;
                    case "0":
                        voltar = true;
                        break;
                    default:
                        Console.WriteLine(">>> Opção inválida <<< \n Digite novamente o número correspondente a opção Desejada: ");
                        Console.ReadKey();
                        break;
                }
            }
        }
        
        private static void AgendarAula()
        {
            Console.Clear();
            Console.WriteLine("[=== AGENDAR AULA ===]");
            
            Aluno aluno = new();
            while (true)
            {
                Console.Write("> Nome do aluno: ");
                try
                {
                    aluno = _alunoService.BuscarAluno(Console.ReadLine()!);
                    break;
                }
                catch (InvalidDataException idex)
                {
                    Console.WriteLine(idex.Message);
                }
                catch (InvalidOperationException ioex)
                {
                    Console.WriteLine(ioex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Aula aula = new();
            while (true)
            {
                Console.Write("> Aula: ");
                try
                {
                    aula = _aulaService.BuscarAula(Console.ReadLine()!);
                    break;
                }
                catch (InvalidDataException idex)
                {
                    Console.WriteLine(idex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            try
            {
                _agendamentoService.CadastrarAgendamento(aluno, aula);
                Console.WriteLine("\nAgendamento realizado com sucesso!");
            }
            catch (InvalidOperationException ioex)
            {
                Console.WriteLine(ioex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private static void ListarAgendamentos()
        {
            Console.Clear();
            Console.WriteLine("[=== LISTA DE AGENDAMENTOS ===]");

            List<Agendamento> agendamentos = [];
            try
            {
                agendamentos = _agendamentoService.ListarAgendamentos();
            }
            catch (DataException dex)
            {
                Console.WriteLine(dex.Message);
            }

            if (agendamentos.Count > 0)
            {
                foreach (var agendamento in agendamentos)
                    Console.WriteLine(agendamento);
                
            }
            else
                Console.WriteLine("Nenhuma aula cadastrada.");
            
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public static void RemoverAgendamento()
        {
            Console.Clear();
            Console.WriteLine("[=== REMOVER AGENDAMENTO ===]");

            Aluno aluno = new();
            while (true)
            {
                Console.Write("> Aluno: ");

                try
                {
                    aluno = _alunoService.BuscarAluno(Console.ReadLine()!);
                    break;
                }
                catch (InvalidDataException idex)
                {
                    Console.WriteLine(idex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Aula aula = new();
            while (true)
            {
                Console.Write("> Aula: ");
                try
                {
                    aula = _aulaService.BuscarAula(Console.ReadLine()!);
                    break;
                }
                catch (InvalidDataException idex)
                {
                    Console.WriteLine(idex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            try
            {
                _agendamentoService.RemoverAgendamento(aluno.Id, aula.Id);
                Console.WriteLine("\nAgendamento removido com sucesso!");
            }
            catch (InvalidOperationException ioex)
            {
                Console.WriteLine(ioex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        #endregion

        #region Relatórios
        private static void MenuRelatorios()
        {
            bool voltar = false;
            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("[=== RELATÓRIOS ===]");
                Console.WriteLine("1 -> Total de Aulas Agendadas por Aluno no Mês");
                Console.WriteLine("2 -> Tipos de Aula Mais Frequentados por Aluno");
                Console.WriteLine("0 -> Menu Anterior");
                Console.Write("\nDigite o número correspondente a opção Desejada: ");

                string opcao = Console.ReadLine() ?? string.Empty;

                switch (opcao)
                {
                    case "1":
                        RelatorioTotalAulasPorAluno();
                        break;
                    case "2":
                        RelatorioTiposAulaMaisFrequentados();
                        break;
                    case "0":
                        voltar = true;
                        break;
                    default:
                        Console.WriteLine(">>> Opção inválida <<< \n Digite novamente o número correspondente a opção Desejada: ");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void RelatorioTotalAulasPorAluno()
        {
            Console.Clear();
            Console.WriteLine("[=== TOTAL DE AULAS AGENDADAS POR ALUNO NO MÊS ===]");

            Aluno aluno = new();
            while (true)
            {
                Console.Write("> Nome do aluno: ");
                try
                {
                    aluno = _alunoService.BuscarAluno(Console.ReadLine()!);
                    break;
                }
                catch (InvalidDataException idex)
                {
                    Console.WriteLine(idex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            int mes = 0;
            int ano = 0;
            while (true)
            {
                try
                {
                    Console.Write("Mês (ex:10): ");
                    mes =_relatorioService.ValidaMes(Console.ReadLine()!);

                    Console.Write("Ano (ex:2025): ");
                    ano = _relatorioService.ValidaAno(Console.ReadLine()!);
                }
                catch (ValidationException vex)
                {
                    Console.WriteLine(vex.Message);
                }
                break;
            }

            int totalAgendamentos = _agendamentoService!.TotalAulasAgendasPorAlunoNoMes(aluno.Id, mes, ano);
            if (totalAgendamentos == 0)
                Console.WriteLine($"Nenhuma aula agendada no mes {mes} de {ano}");
            else
            {
                Console.WriteLine($"\nAluno: {aluno.Nome}");
                Console.WriteLine($"Plano: {aluno.TipoPlano}");
                Console.WriteLine($"Total de aulas agendadas em {mes}/{ano}: {totalAgendamentos}");
                Console.WriteLine($"Limite do plano: {aluno.QtdAulasPlano}");
                Console.WriteLine($"Aulas restantes: {aluno.QtdAulasPlano - totalAgendamentos}");
            }
            
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private static void RelatorioTiposAulaMaisFrequentados()
        {
            Console.Clear();
            Console.WriteLine("[=== TIPOS DE AULA MAIS FREQUENTADOS POR ALUNO ===]");

            Aluno aluno = new();
            while (true)
            {
                Console.Write("> Nome do aluno: ");

                try
                {
                    aluno = _alunoService.BuscarAluno(Console.ReadLine()!);
                }
                catch (InvalidDataException idex)
                {
                    Console.WriteLine($"Erro ao buscar: {idex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro inesperado: {ex.Message}");
                }

                break;
            }
            Console.WriteLine($"---> Aluno: {aluno.Nome}");

            List<FrequenciaAula> aulaFav = _relatorioService.TiposAulaMaisFrequentadosPorAluno(aluno.Id);
            Console.WriteLine("Tipos de aula mais frequentados:");
            for (int pos = 0; pos<aulaFav.Count;  pos++)
            { 
                Console.WriteLine($"{pos+1} - Aula:{aulaFav[pos].TipoAula} frequentado {aulaFav[pos].QtdAgendamento} vezes");
            }


            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        #endregion
    }
}