using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProLearning.Api.ApiKey;
using ProLearning.Api.Database;
using ProLearning.Api.Endpoints.EducationLevels;
using ProLearning.Api.Endpoints.Goals;
using ProLearning.Api.Endpoints.InterestAreas;
using ProLearning.Api.Endpoints.LearningActivities;
using ProLearning.Api.Endpoints.Recommendations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddProblemDetails();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

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

app.MapLearningActivityEndpoints();
app.MapRecommendationsEndpoints();
app.MapEducationLevelEndpoints();
app.MapInterestAreaEndpoints();
app.MapGoalEndpoints();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.Run();