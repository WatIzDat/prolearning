using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProLearning.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddInterestAreaAndGoalEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EducationLevelLearningActivity",
                columns: table => new
                {
                    EducationLevelsId = table.Column<int>(type: "integer", nullable: false),
                    LearningActivitiesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationLevelLearningActivity", x => new { x.EducationLevelsId, x.LearningActivitiesId });
                    table.ForeignKey(
                        name: "FK_EducationLevelLearningActivity_EducationLevels_EducationLev~",
                        column: x => x.EducationLevelsId,
                        principalTable: "EducationLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationLevelLearningActivity_LearningActivities_LearningA~",
                        column: x => x.LearningActivitiesId,
                        principalTable: "LearningActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InterestArea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestArea", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoalScoreBoost",
                columns: table => new
                {
                    LearningActivityId = table.Column<int>(type: "integer", nullable: false),
                    GoalId = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalScoreBoost", x => new { x.GoalId, x.LearningActivityId });
                    table.ForeignKey(
                        name: "FK_GoalScoreBoost_Goal_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoalScoreBoost_LearningActivities_LearningActivityId",
                        column: x => x.LearningActivityId,
                        principalTable: "LearningActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterestAreaScoreBoost",
                columns: table => new
                {
                    LearningActivityId = table.Column<int>(type: "integer", nullable: false),
                    InterestAreaId = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestAreaScoreBoost", x => new { x.InterestAreaId, x.LearningActivityId });
                    table.ForeignKey(
                        name: "FK_InterestAreaScoreBoost_InterestArea_InterestAreaId",
                        column: x => x.InterestAreaId,
                        principalTable: "InterestArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterestAreaScoreBoost_LearningActivities_LearningActivityId",
                        column: x => x.LearningActivityId,
                        principalTable: "LearningActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducationLevelLearningActivity_LearningActivitiesId",
                table: "EducationLevelLearningActivity",
                column: "LearningActivitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_GoalScoreBoost_LearningActivityId",
                table: "GoalScoreBoost",
                column: "LearningActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_InterestAreaScoreBoost_LearningActivityId",
                table: "InterestAreaScoreBoost",
                column: "LearningActivityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationLevelLearningActivity");

            migrationBuilder.DropTable(
                name: "GoalScoreBoost");

            migrationBuilder.DropTable(
                name: "InterestAreaScoreBoost");

            migrationBuilder.DropTable(
                name: "Goal");

            migrationBuilder.DropTable(
                name: "InterestArea");
        }
    }
}
