using Microsoft.EntityFrameworkCore;
using ProLearning.Api.Database;
using ProLearning.Api.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.MapGet("/recommendations", async (ApplicationDbContext dbContext, string educationLevel, string grade, string[] interestAreas, string[] goals, int limit) =>
{
    var learningActivities =
        await dbContext.EducationLevels
            .Where(l => l.Name.Equals(educationLevel, StringComparison.OrdinalIgnoreCase))
            .SelectMany(l => l.LearningActivities)
            .Select(a => new
            {
                a.Name,
                Score = 
                    a.InterestAreaScoreBoosts
                        .Where(e => ((IEnumerable<string>)interestAreas).Contains(e.InterestArea.Name, StringComparer.OrdinalIgnoreCase))
                        .Select(e => e.Score)
                        .Sum() + 
                    a.GoalScoreBoosts
                        .Where(e => ((IEnumerable<string>)goals).Contains(e.Goal.Name, StringComparer.OrdinalIgnoreCase))
                        .Select(e => e.Score)
                        .Sum()
            })
            .OrderBy(a => a.Score)
            .Take(limit)
            .ToListAsync();

    return learningActivities;
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}