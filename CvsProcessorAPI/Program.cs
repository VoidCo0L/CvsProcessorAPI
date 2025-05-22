using CsvProcessorAPI.Queue;
using CsvProcessorAPI.Services;
using CsvProcessorAPI.Worker;
using CvsProcessorAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddFile("logs/app-log-{Date}.txt"); //line 10-12 log to File instead of console

builder.Services.AddSingleton<IFileProcessingQueue, InMemoryFileProcessingQueue>();
builder.Services.AddHostedService<FileProcessingWorker>();
builder.Services.AddSingleton<ICsvValidator, SimpleCsvValidator>();
builder.Services.AddSingleton<IErrorQueue, InMemoryErrorQueue>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
