using McpServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

//congiurar los logs para que no rompan el stdio
builder.Logging.AddConsole(consoleLogOptions =>
{
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});

//configuro el servidor 
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

//agrego el cliente http al contenedor de dependencias
builder.Services.AddHttpClient("TodoApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7027");//esto se podria mandar a un appsettings.json
});


await builder.Build().RunAsync();