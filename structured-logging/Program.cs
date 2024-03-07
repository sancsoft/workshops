using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Grafana.Loki;
using StructuredLogging;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting StructuredLogging");

    var builder = Host.CreateApplicationBuilder(args);
    builder.Logging.AddSerilog(new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console(new RenderedCompactJsonFormatter())
        .WriteTo.GrafanaLoki("http://ubuntu-dev:3100", propertiesAsLabels: new List<string>() { "app", "version" })
        .Enrich.FromLogContext()
        .Enrich.WithProperty("app", "StructuredLogging")
        .Enrich.WithProperty("version", "v1.2.3")
        .CreateLogger());

    builder.Services.AddHostedService<Worker>();

    var host = builder.Build();
    host.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
