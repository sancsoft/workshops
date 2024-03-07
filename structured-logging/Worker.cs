namespace StructuredLogging;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var random = new Random();

        Dictionary<int, Guid> ids = new();

        while (!stoppingToken.IsCancellationRequested)
        {
            for (var i = 0; i < 10; i++)
            {
                Guid id;
                if(!ids.ContainsKey(i))
                {
                    ids.Add(i, Guid.NewGuid());
                }

                id = ids[i];
                
                using (_logger.BeginScope(new Dictionary<string, object>() { { "someRecordId", id } }))
                {
                    try
                    {
                        _logger.LogInformation("Attempting to process index {index}", i);

                        if(random.Next(100) > 75)
                        {
                            throw new Exception("Random exception!");
                        }

                        _logger.LogInformation("Worker {index} running at: {time}", i, DateTimeOffset.Now);
                        await Task.Delay(1000, stoppingToken);
                    }
                    catch(Exception ex)
                    {
                        _logger.LogError(ex, "Error processing index {index}", i);
                    }
                }
            }

            await Task.Delay(10000, stoppingToken);
        }
    }
}
