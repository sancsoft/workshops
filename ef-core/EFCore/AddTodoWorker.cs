using EFCore.Models;

namespace EFCore;

public class AddTodoWorker : BackgroundService
{
    private readonly ILogger<AddTodoWorker> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public AddTodoWorker(ILogger<AddTodoWorker> logger, IServiceScopeFactory serviceScopeFactory)
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

            var todoList = new ToDoList();
            todoList.Name = $"{Environment.UserName} Todo";

            todoList.Items.Add(new ToDoItem()
            {
                ToDo = "Item 1"
            });

            todoList.Items.Add(new ToDoItem()
            {
                ToDo = "Item 2"
            });

            context.ToDoLists.Add(todoList);

            _logger.LogInformation("Creating ToDoList {Name} with ID {Id}", todoList.Name, todoList.Id);

            await context.SaveChangesAsync(stoppingToken);

            context.ToDoItems.Add(new ToDoItem()
            {
                ToDoListId = todoList.Id,
                ToDo = "Item 3"
            });

            await context.SaveChangesAsync(stoppingToken);

            await Task.Delay(10000, stoppingToken);
        }
    }
}
