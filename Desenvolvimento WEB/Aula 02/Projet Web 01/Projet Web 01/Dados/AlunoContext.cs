using Microsoft.EntityFrameworkCore;
using Projet_Web_01.Classes.Entidades;

namespace Projet_Web_01.Dados
{
    public class AlunoContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=ECFP5121319369\SQLEXPRESS;Database=Aluno;Trusted_Connection=True;TrustServerCertificate=True;"
                );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>
            (
                entity =>
                {
                    entity.HasKey(e => e.ID);
                    entity.Property(e => e.Nome).IsRequired();
                    entity.Property(e => e.Email);
                    entity.Property(e => e.Senha);
                    entity.Property(e => e.CursoID).IsRequired();
                    entity.Property(e => e.RA).IsRequired();
                    entity.Property(e => e.StatusAction).IsRequired();
                    entity.Property(e => e.StatusWIFI).IsRequired();
                }
            );
            
        }


    }

}
