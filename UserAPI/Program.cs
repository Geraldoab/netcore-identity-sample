using Serilog;
using UserAPI;
using UserAPI.Log;

var builder = WebApplication.CreateBuilder(args);

SerilogHelper.ConfigureLogging(Log.Logger);
builder.Host.UseSerilog();

// Add services to the container.

var startup = new Startup(builder.Configuration); // My custom startup class.

startup.ConfigureServices(builder.Services); // Add services to the container.

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();
