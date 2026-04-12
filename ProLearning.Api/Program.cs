using Microsoft.EntityFrameworkCore;
using ProLearning.Api.ApiKey;
using ProLearning.Api.Database;
using ProLearning.Api.Domain;
using ProLearning.Api.Domain.Recommendation;
using ProLearning.Api.Requests;

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

app.MapPost("/learningactivity", async (ApplicationDbContext dbContext, CreateLearningActivityRequest request) =>
{
    List<EducationLevel> educationLevels = await dbContext.EducationLevels
        .Where(l => request.EducationLevels.Contains(l.Name))
        .ToListAsync();

    List<string> interestAreaNames = request.InterestAreaScoreBoosts.Select(b => b.InterestArea).ToList();

    List<InterestArea> interestAreas = await dbContext.InterestAreas
        .Where(a => interestAreaNames.Contains(a.Name))
        .ToListAsync();

    List<InterestAreaScoreBoost> interestAreaScoreBoosts = interestAreas
        .Join(
            request.InterestAreaScoreBoosts,
            a => a.Name,
            b => b.InterestArea,
            (a, b) => new InterestAreaScoreBoost { InterestArea = a, Score = b.Score },
            StringComparer.OrdinalIgnoreCase)
        .ToList();
    
    Console.WriteLine(interestAreaScoreBoosts.Count);

    List<string> goalNames = request.GoalScoreBoosts.Select(b => b.Goal).ToList();

    List<Goal> goals = await dbContext.Goals
        .Where(g => goalNames.Contains(g.Name))
        .ToListAsync();

    List<GoalScoreBoost> goalScoreBoosts = goals
        .Join(
            request.GoalScoreBoosts,
            g => g.Name,
            b => b.Goal,
            (g, b) => new GoalScoreBoost { Goal = g, Score = b.Score },
            StringComparer.OrdinalIgnoreCase)
        .ToList();
    
    LearningActivity learningActivity = new()
    {
        Name = request.Name,
        EducationLevels = educationLevels,
        InterestAreaScoreBoosts = interestAreaScoreBoosts,
        GoalScoreBoosts = goalScoreBoosts
    };

    dbContext.LearningActivities.Add(learningActivity);
    
    await dbContext.SaveChangesAsync();
}).AddEndpointFilter<ApiKeyEndpointFilter>();

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
                        .Where(e => ((IEnumerable<string>)interestAreas).Contains(e.InterestArea.Name))
                        .Select(e => e.Score)
                        .Sum() + 
                    a.GoalScoreBoosts
                        .Where(e => ((IEnumerable<string>)goals).Contains(e.Goal.Name))
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