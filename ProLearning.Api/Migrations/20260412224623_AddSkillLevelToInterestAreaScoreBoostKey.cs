using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProLearning.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSkillLevelToInterestAreaScoreBoostKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InterestAreaScoreBoost",
                table: "InterestAreaScoreBoost");

            migrationBuilder.DropIndex(
                name: "IX_InterestAreaScoreBoost_LearningActivityId",
                table: "InterestAreaScoreBoost");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InterestAreaScoreBoost",
                table: "InterestAreaScoreBoost",
                columns: new[] { "LearningActivityId", "InterestAreaId", "SkillLevel" });

            migrationBuilder.CreateIndex(
                name: "IX_InterestAreaScoreBoost_InterestAreaId",
                table: "InterestAreaScoreBoost",
                column: "InterestAreaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InterestAreaScoreBoost",
                table: "InterestAreaScoreBoost");

            migrationBuilder.DropIndex(
                name: "IX_InterestAreaScoreBoost_InterestAreaId",
                table: "InterestAreaScoreBoost");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InterestAreaScoreBoost",
                table: "InterestAreaScoreBoost",
                columns: new[] { "InterestAreaId", "LearningActivityId" });

            migrationBuilder.CreateIndex(
                name: "IX_InterestAreaScoreBoost_LearningActivityId",
                table: "InterestAreaScoreBoost",
                column: "LearningActivityId");
        }
    }
}
