using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace alunos.Service
{
    public abstract class HostedService : IHostedService
    {
        private Task _executingTask;
        private CancellationTokenSource _cts;
        private object serviceProvider;

        protected HostedService(object serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _executingTask = ExecuteAsync(_cts.Token);
            return _executingTask.IsCompleted ? _executingTask : Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask == null)
            {
                return;
            }
            _cts.Cancel();
            await Task.WhenAny(_executingTask, Task.Delay(-1, cancellationToken));
            cancellationToken.ThrowIfCancellationRequested();
        }

        public abstract Task ExecuteAsync(CancellationToken cancellationToken);
    }
}