using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProLearning.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUrlToLearningActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "LearningActivities",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "LearningActivities");
        }
    }
}
