namespace Thon.App.StartupTasks;

internal interface IStartupTask
{
    public Task Execute(CancellationToken cancellationToken = default);
}
