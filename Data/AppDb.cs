using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using agendadorAulas.Model;

namespace agendadorAulas.Data
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions<AppDb> options) : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Aula> Aulas { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Duplicidade de alunos com o mesmo nome
            modelBuilder.Entity<Aluno>()
                .HasIndex(a => a.Nome)
                .IsUnique();

            //Duplicidade do aluno se cadastrar na mesma aula
            modelBuilder.Entity<Agendamento>()
                .HasIndex(a => new { a.AlunoId, a.AulaId, a.DataHora })
                .IsUnique();

            //Configuração de relacionamentos de tabelas e FK
            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Aluno)
                .WithMany()
                .HasForeignKey(a => a.AlunoId);

            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Aula)
                .WithMany()
                .HasForeignKey(a => a.AulaId);
        }
    }
}