using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alunos.Data;
using alunos.Model;
using Microsoft.EntityFrameworkCore;

namespace alunos.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly AlunoContext _context;

        public AlunoRepository(AlunoContext context)
        {
            _context = context;
        }
        public void AdicionaAluno(Aluno aluno)
        {
            _context.Add(aluno);
        }

        public async Task<bool> AdicionaMatriculas(List<Aluno> matriculas)
        {
            if (matriculas.Count > 0)
            {
                await _context.AddRangeAsync(matriculas);
            }
                return await _context.SaveChangesAsync() > 0;
        }

        public void AtualizaAluno(Aluno aluno)
        {
            _context.Update(aluno);
        }

        public async Task<Aluno> BuscaAluno(int id)
        {
            return await _context.Alunos.Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Aluno>> BuscaMatriculasPorNome(string nome)
        {
            return await _context.Alunos.Where(x => x.Nome == nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Aluno>> ListarAlunos()
        {
            return await _context.Alunos.ToListAsync();
        }

        public void RemoveAluno(Aluno aluno)
        {
            _context.Remove(aluno);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> TotalDeMatriculas()
        {
            return await _context.Alunos.CountAsync();
        }
    }
}