using CursoIdiomasAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace CursoIdiomasAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>()
                .HasMany(e => e.Turmas)
                .WithMany(e => e.Alunos)
                .UsingEntity(
                    l => l.HasOne(typeof(Turma)).WithMany().HasForeignKey("TurmaId"),
                    r => r.HasOne(typeof(Aluno)).WithMany().HasForeignKey("AlunoId"));
        }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
    }
}
