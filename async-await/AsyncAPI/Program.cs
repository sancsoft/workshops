var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", async (ILogger<Program> logger) =>
{
    await AsyncIterate(logger, 100);
});

app.MapGet("/ct", async (ILogger<Program> logger, CancellationToken token) =>
{
    await AsyncIterate(logger, 100, token);
});

app.Run();

async Task AsyncIterate(ILogger<Program> logger, int iterations, CancellationToken token = default)
{
    for(var i = 0; i < iterations; i++)
    {
        // Alternatively you can cleanly exit depending on circumstances by checking `token.IsCancellationRequested` 
        // and breaking out of the loop. This can be handy if you need to perform some cleanup.
        token.ThrowIfCancellationRequested();

        logger.LogInformation("Processing {i}", i);
        await Task.Delay(100, token);
    }
}