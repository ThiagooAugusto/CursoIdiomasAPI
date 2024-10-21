using CursoIdiomasAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CursoIdiomasAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
    }
}
