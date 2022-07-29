using System;
using System.Linq;
using System.Threading.Tasks;
using alunos.Model;
using alunos.Repository;
using Microsoft.AspNetCore.Mvc;

namespace alunos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoRepository _repository;

        public AlunoController(IAlunoRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Aluno aluno)
        {
            _repository.AdicionaAluno(aluno);
            return await _repository.SaveChangesAsync()
                ? Ok("Aluno adicionado com sucesso")
                : BadRequest("Erro ao adicionar aluno");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var alunos = await _repository.ListarAlunos();
            return alunos.Any()
                ? Ok(alunos)
                : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var aluno = await _repository.BuscaAluno(id);
            return aluno != null
                ? Ok(aluno)
                : NotFound("Aluno não encontrado");
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Put(int id, Aluno aluno)
        {
            var alunoDb = await _repository.BuscaAluno(id);
            if (alunoDb == null) return NotFound("Aluno não encontrado");

            alunoDb.Nome = aluno.Nome ?? alunoDb.Nome;
            alunoDb.DataNascimento = aluno.DataNascimento != new DateTime()
                ? aluno.DataNascimento : alunoDb.DataNascimento;

            _repository.AtualizaAluno(alunoDb);

            return await _repository.SaveChangesAsync()
               ? Ok("Aluno atualizado com sucesso")
               : BadRequest("Erro ao atualizar aluno");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var alunoDb = await _repository.BuscaAluno(id);
            if (alunoDb == null) return NotFound("Aluno não encontrado");

            _repository.RemoveAluno(alunoDb);
            return await _repository.SaveChangesAsync()
               ? Ok("Aluno removido com sucesso")
               : BadRequest("Erro ao remover aluno");
        }
    }
}