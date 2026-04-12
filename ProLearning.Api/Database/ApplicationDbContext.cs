using Microsoft.EntityFrameworkCore;
using ProLearning.Api.Domain;

namespace ProLearning.Api.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<LearningActivity> LearningActivities { get; set; }
    public DbSet<EducationLevel> EducationLevels { get; set; }
}