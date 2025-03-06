using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using QuestionsAppSimple.Properties.DB;

var builder = WebApplication.CreateBuilder(args);
// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = new SqliteConnectionStringBuilder() { DataSource = "Production.db" }.ToString();
builder.Services.AddDbContext<QuestionsContext>(x => x.UseSqlite(connectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
    scope.ServiceProvider.GetRequiredService<QuestionsContext>().Database.EnsureCreated();


// API Routes

app.MapGet("api/questions", async (QuestionsContext context)
    => await context.Questions.ToListAsync());

app.MapPost("api/questions/", async (QuestionsContext context, string content) =>
{
    if (string.IsNullOrWhiteSpace(content))
        return Results.BadRequest("The Question Content can not be empty");

    context.Questions.Add(new Question { Content = content });
    await context.SaveChangesAsync();
    return Results.Ok();
});

app.MapPost("api/questions/{id:int}/vote", async (QuestionsContext context, int id) =>
{
    var question = await context.Questions.FirstOrDefaultAsync(q => q.Id == id);
    if (question is null)
        return Results.BadRequest("Invalid Question Id");

    question.Votes++;
    await context.SaveChangesAsync();
    return Results.Ok();
});

// Enable swagger and SwaggerUI in Developer Mode.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Run();
