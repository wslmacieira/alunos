using alunos.Model;
using Microsoft.EntityFrameworkCore;

namespace alunos.Data
{
    public class AlunoContext : DbContext
    {
        public AlunoContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var aluno = modelBuilder.Entity<Aluno>();
            aluno.ToTable("tb_aluno");
            aluno.HasKey(x => x.Id);
            aluno.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            aluno.Property(x => x.Nome).HasColumnName("nome").IsRequired();
            aluno.Property(x => x.DataNascimento).HasColumnName("data_nascimento");
        }
    }
}