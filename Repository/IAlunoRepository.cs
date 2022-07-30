using System.Collections.Generic;
using System.Threading.Tasks;
using alunos.Model;

namespace alunos.Repository
{
    public interface IAlunoRepository
    {
        Task<IEnumerable<Aluno>> ListarAlunos();
        Task<int> TotalDeMatriculas();
        Task<Aluno> BuscaAluno(int id);
        Task<IEnumerable<Aluno>> BuscaMatriculasPorNome(string nome);
        void AdicionaAluno(Aluno aluno);
        void AtualizaAluno(Aluno aluno);
        Task<bool> AdicionaMatriculas(List<Aluno> alunos);
        void RemoveAluno(Aluno aluno);
        Task<bool> SaveChangesAsync();
    }
}