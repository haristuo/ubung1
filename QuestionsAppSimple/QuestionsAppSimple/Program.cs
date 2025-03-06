using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using QuestionsAppSimple.Properties.DB;

var builder = WebApplication.CreateBuilder(args);
// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
    scope.ServiceProvider.GetRequiredService<QuestionsContext>().Database.EnsureCreated();
var connectionString = new SqliteConnectionStringBuilder() { DataSource = "Production.db" }.ToString();
builder.Services.AddDbContext<QuestionsContext>(x => x.UseSqlite(connectionString));

app.MapGet("/", () => "Hello World!");
// Enable swagger and SwaggerUI in Developer Mode.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Run();