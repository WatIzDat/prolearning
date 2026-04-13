using Microsoft.EntityFrameworkCore;
using ProLearning.Api;
using ProLearning.Api.ApiKey;
using ProLearning.Api.Database;
using ProLearning.Api.Endpoints.LearningActivities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddTransient<IApiKeyValidator, ApiKeyValidator>();

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

app.MapLearningActivityEndpoints();

app.MapGet("/recommendations", async (ApplicationDbContext dbContext, string educationLevel, string[] interestAreas, int[] skillLevels, string[] goals, int limit) =>
{
    if (interestAreas.Length != skillLevels.Length)
        return Results.BadRequest();
    
    var learningActivities =
        await dbContext.EducationLevels
            .Where(l => l.Name == educationLevel)
            .SelectMany(l => l.LearningActivities)
            .Select(a => new
            {
                name = a.Name,
                score = 
                    a.InterestAreaScoreBoosts
                        .Where(e => ((IEnumerable<string>)interestAreas).Contains(e.InterestArea.Name) && skillLevels[interestAreas.ToList().IndexOf(e.InterestArea.Name)] == (int)e.SkillLevel)
                        .Select(e => e.Score)
                        .Sum() + 
                    a.GoalScoreBoosts
                        .Where(e => ((IEnumerable<string>)goals).Contains(e.Goal.Name))
                        .Select(e => e.Score)
                        .Sum(),
                scoreBreakdown = new
                {
                    interestAreas = 
                        a.InterestAreaScoreBoosts
                            .Where(e => ((IEnumerable<string>)interestAreas).Contains(e.InterestArea.Name) && skillLevels[interestAreas.ToList().IndexOf(e.InterestArea.Name)] == (int)e.SkillLevel)
                            .Select(e => new { interestArea = e.InterestArea.Name, skillLevel = e.SkillLevel, score = e.Score }),
                    goals =
                        a.GoalScoreBoosts
                            .Where(e => ((IEnumerable<string>)goals).Contains(e.Goal.Name))
                            .Select(e => new { interestArea = e.Goal.Name, score = e.Score })
                }
            })
            .OrderBy(a => a.score)
            .Take(limit)
            .ToListAsync();

    return Results.Ok(learningActivities);
});

app.Run();

namespace ProLearning.Api
{
    record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}