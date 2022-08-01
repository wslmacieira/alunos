using System;
using System.Linq;
using System.Threading.Tasks;
using alunos.Model;
using alunos.Repository;
using alunos.Repository.Service;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace alunos.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoRepository _repository;
        private readonly AlunoService _alunoService = new AlunoService();

        public AlunoController(IAlunoRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [SwaggerOperation("Cadastro novo aluno")]
        [SwaggerResponse(200, "Aluno adicionado com sucesso")]
        [SwaggerResponse(400, "Erro ao adicionar aluno")]
        public async Task<IActionResult> CadastraAluno(Aluno aluno)
        {
            _repository.AdicionaAluno(aluno);
            return await _repository.SaveChangesAsync()
                ? Ok("Aluno adicionado com sucesso")
                : BadRequest("Erro ao adicionar aluno");
        }

        [HttpGet]
        [SwaggerOperation("Lista alunos")]
        [SwaggerResponse(200, "Retorna uma lista de alunos")]
        [SwaggerResponse(204, "No Content")]
        public async Task<IActionResult> ListaAlunos()
        {
            var alunos = await _repository.ListarAlunos();

            return alunos.Any()
                ? Ok(alunos)
                : NoContent();
        }

        [HttpPost("matriculas/{quantidade}")]
        [SwaggerOperation("Cadastro de novas matriculas")]
        [SwaggerResponse(200, "Retorna a lista de matriculas cadastrada")]
        [SwaggerResponse(204, "No Content")]
        public async Task<IActionResult> CadastraNovosAlunos(int quantidade)
        {
            var matriculas = await _alunoService.BuscaNovosAlunos(quantidade);
            await _repository.AdicionaMatriculas(matriculas);
            return matriculas.Any()
                ? Ok(matriculas)
                : NoContent();
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Busca aluno pelo id")]
        [SwaggerResponse(200, "Retorna aluno encontrado pelo id")]
        [SwaggerResponse(404, "Aluno não encontrado")]
        public async Task<IActionResult> BuscaAlunoPorId(int id)
        {
            var aluno = await _repository.BuscaAluno(id);
            return aluno != null
                ? Ok(aluno)
                : NotFound("Aluno não encontrado");
        }

        [HttpGet("busca-matriculas/{nome}")]
        [SwaggerOperation("Busca alunos pelo nome")]
        [SwaggerResponse(200, "Retorna alunos encontrados pelo nome")]
        [SwaggerResponse(404, "Aluno não encontrado")]
        public async Task<IActionResult> BuscaAlunosPorNome(string nome)
        {
            var aluno = await _repository.BuscaMatriculasPorNome(nome);
            return aluno != null
                ? Ok(aluno)
                : NotFound("Aluno não encontrado");
        }

        [HttpPut("{id}")]
        [SwaggerOperation("Atualiza aluno")]
        [SwaggerResponse(200, "Aluno atualizado com sucesso")]
        [SwaggerResponse(404, "Erro ao atualizar aluno")]

        public async Task<IActionResult> Atualiza(int id, Aluno aluno)
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
        [SwaggerOperation("Remove aluno")]
        [SwaggerResponse(200, "Aluno removido com sucesso")]
        [SwaggerResponse(404, "Erro ao remover aluno")]
        public async Task<IActionResult> Remove(int id)
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