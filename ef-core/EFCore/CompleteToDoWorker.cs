using EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore;

public class CompleteToDoWorker : BackgroundService
{
    private readonly ILogger<CompleteToDoWorker> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CompleteToDoWorker(ILogger<CompleteToDoWorker> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await using var scope = _serviceScopeFactory.CreateAsyncScope();
            await using var context = scope.ServiceProvider.GetRequiredService<ToDoContext>();

            var cutoff = DateTime.UtcNow.AddSeconds(-30);
            var oldTodoListsWithIncompleteItems = context.ToDoLists
                .Include(t => t.Items)
                .Where(t => t.CreatedAt <= cutoff && t.Items.Any(x => !x.Completed));

            foreach (var todoList in oldTodoListsWithIncompleteItems)
            {
                foreach (var item in todoList.Items)
                {
                    _logger.LogInformation("Completing todo item {ToDo} for list {ListName}", item.ToDo, todoList.Name);
                    await Task.Delay(1000, stoppingToken);

                    item.Completed = true;
                }
            }

            await context.SaveChangesAsync(stoppingToken);
            await Task.Delay(1000, stoppingToken);
        }
    }
}
