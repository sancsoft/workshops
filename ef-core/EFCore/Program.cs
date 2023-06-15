using EFCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddDbContext<ToDoContext>();
        services.AddHostedService<AddTodoWorker>();
        services.AddHostedService<CompleteToDoWorker>();
    })
    .Build();

host.Run();
