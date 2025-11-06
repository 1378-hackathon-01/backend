using Microsoft.Extensions.Hosting;
using Thon.App.StartupTasks;

namespace Thon.App.Services.Hosted;

/// <summary>
/// Hosted-сервис, который выполняет все IStartupTask перед запуском основных IHostedService.
/// Обеспечивает правильную последовательность инициализации сервисов.
/// </summary>
internal class HostedStartupTasksService(IEnumerable<IStartupTask> startupTasks) : IHostedService
{
    private readonly IEnumerable<IStartupTask> _startupTasks = startupTasks;

    /// <summary>
    /// Запуск сервиса. Выполняет все задачи инициализации.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Асинхронная задача.</returns>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (var task in _startupTasks)
            await task.Execute(cancellationToken);
    }

    /// <summary>
    /// Остановка сервиса.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Завершённая задача.</returns>
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
