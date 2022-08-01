using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using alunos.Repository;
using alunos.Repository.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace alunos.Service
{
    public class MatriculaBackgroundService : HostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MatriculaBackgroundService> _logger;
        private readonly AlunoService alunoService = new AlunoService();
        private readonly IAlunoRepository _repository;
        private int _tempo = 0;
        private int _quantidade = 0;
        public MatriculaBackgroundService(IServiceProvider serviceProvider, ILogger<MatriculaBackgroundService> logger) : base(serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        public async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var scopedService = scope.ServiceProvider.GetRequiredService<IAlunoRepository>();
                    var alunos = await alunoService.BuscaNovosAlunos(_quantidade);
                    var total = await scopedService.TotalDeMatriculas();
                    await scopedService.AdicionaMatriculas(alunos);
                    _logger.LogInformation("time {dateTime} MatriculaService: ExecuteAsync Total matriculas:{total}", DateTime.Now, total);
                    await Task.Delay(TimeSpan.FromSeconds(_tempo), cancellationToken);
                }
            }
        }
        public void ConfiguraTask(int tempo, int quantidade)
        {
            _tempo = tempo;
            _quantidade = quantidade;
        }
    }
}