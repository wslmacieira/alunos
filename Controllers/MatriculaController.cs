using System.Threading.Tasks;
using alunos.Model;
using alunos.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace alunos.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    public class MatriculaController : Controller
    {
        private readonly ILogger<MatriculaController> _logger;
        private readonly MatriculaBackgroundService _myBackgroundService;

        public MatriculaController(ILogger<MatriculaController> logger, HostedService hostedService)
        {
            _logger = logger;
            _myBackgroundService = hostedService as MatriculaBackgroundService;
        }

        [HttpPost]
        [SwaggerOperation("Inicia Job em Background para adicionar novas matriculas")]
        [SwaggerResponse(200, "Job iniciado")]
        public async Task<IActionResult> IniciaJob([FromBody] MatriculaTask request)
        {
            // Validação para limite de tempo e quantidade de matriculas para não extrapolar o banco de dados
            if ((request.tempo < 5 || request.tempo > 86400) || (request.quantidade < 1 || request.quantidade > 100))
            {
                return BadRequest("tempo deve ser ente 5 a 86400 segundos e quantidade 1 a 100");
            }
            _myBackgroundService.ConfiguraTask(request.tempo, request.quantidade);
            await _myBackgroundService.StartAsync(new System.Threading.CancellationToken());
            return Ok("Job iniciado");
        }

        [HttpPost]
        [SwaggerOperation("Encerra Job em Background que adiciona novas matriculas")]
        [SwaggerResponse(200, "Job finalizado")]
        public async Task<IActionResult> FinalizaJob()
        {
            // await _repository.AdicionaMatriculas(matriculas);
            await _myBackgroundService.StopAsync(new System.Threading.CancellationToken());
            return Ok("Job finalizado");
        }
    }
}
