using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProLearning.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddNameIndexesToEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LearningActivities_Name",
                table: "LearningActivities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InterestAreas_Name",
                table: "InterestAreas",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Goals_Name",
                table: "Goals",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducationLevels_Name",
                table: "EducationLevels",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LearningActivities_Name",
                table: "LearningActivities");

            migrationBuilder.DropIndex(
                name: "IX_InterestAreas_Name",
                table: "InterestAreas");

            migrationBuilder.DropIndex(
                name: "IX_Goals_Name",
                table: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_EducationLevels_Name",
                table: "EducationLevels");
        }
    }
}
