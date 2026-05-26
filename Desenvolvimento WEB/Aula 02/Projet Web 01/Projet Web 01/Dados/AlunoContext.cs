using Microsoft.EntityFrameworkCore;
using Projet_Web_01.Classes.Entidades;

namespace Projet_Web_01.Dados
{
    public class AlunoContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }
    }
}
