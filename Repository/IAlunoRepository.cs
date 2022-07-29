using System.Collections.Generic;
using System.Threading.Tasks;
using alunos.Model;

namespace alunos.Repository
{
    public interface IAlunoRepository
    {
        Task<IEnumerable<Aluno>> ListarAlunos();
        Task<Aluno> BuscaAluno(int id);
        void AdicionaAluno(Aluno aluno);
        void AtualizaAluno(Aluno aluno);
        void RemoveAluno(Aluno aluno);
        Task<bool> SaveChangesAsync();
    }
}