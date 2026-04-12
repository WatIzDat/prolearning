using Microsoft.EntityFrameworkCore;
using ProLearning.Api.Domain;
using ProLearning.Api.Domain.Recommendation;

namespace ProLearning.Api.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<LearningActivity> LearningActivities { get; set; }
    public DbSet<EducationLevel> EducationLevels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LearningActivity>()
            .HasMany(e => e.InterestAreas)
            .WithMany(e => e.LearningActivities)
            .UsingEntity<InterestAreaScoreBoost>();
        
        modelBuilder.Entity<LearningActivity>()
            .HasMany(e => e.Goals)
            .WithMany(e => e.LearningActivities)
            .UsingEntity<GoalScoreBoost>();
    }
}