var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", async (ILogger<Program> logger) =>
{
    await AsyncIterate(logger, 100, false);
});

app.MapGet("/ct", async (ILogger<Program> logger, CancellationToken token) =>
{
    await AsyncIterate(logger, 100, false, token);
});

app.MapGet("/parallel", async (ILogger<Program> logger) =>
{
    await AsyncIterate(logger, 100, true);
});

app.MapGet("/parallel/ct", async (ILogger<Program> logger, CancellationToken token) =>
{
    await AsyncIterate(logger, 100, true, token);
});

app.Run();

async Task AsyncIterate(ILogger<Program> logger, int iterations, bool parallel = false, CancellationToken token = default)
{
    if(parallel)
    {
        await Parallel.ForEachAsync(Enumerable.Range(0, 100), new ParallelOptions() {
            CancellationToken = token,
            MaxDegreeOfParallelism = 10
        }, async (i, ct) => {
            logger.LogInformation("Processing {i}", i);
            await Task.Delay(1000, token);
        });
    }
    else
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
}