using Microsoft.EntityFrameworkCore;

namespace alunos.Data
{
    public class AlunoContext : DbContext
    {
        public AlunoContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Aluno> Alunos { get; set; }
    }
}