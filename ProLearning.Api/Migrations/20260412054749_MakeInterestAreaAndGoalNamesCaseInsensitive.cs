using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProLearning.Api.Migrations
{
    /// <inheritdoc />
    public partial class MakeInterestAreaAndGoalNamesCaseInsensitive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE EXTENSION citext;");
            
            migrationBuilder.DropForeignKey(
                name: "FK_GoalScoreBoost_Goal_GoalId",
                table: "GoalScoreBoost");

            migrationBuilder.DropForeignKey(
                name: "FK_InterestAreaScoreBoost_InterestArea_InterestAreaId",
                table: "InterestAreaScoreBoost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InterestArea",
                table: "InterestArea");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Goal",
                table: "Goal");

            migrationBuilder.RenameTable(
                name: "InterestArea",
                newName: "InterestAreas");

            migrationBuilder.RenameTable(
                name: "Goal",
                newName: "Goals");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:citext", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InterestAreas",
                type: "citext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Goals",
                type: "citext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InterestAreas",
                table: "InterestAreas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Goals",
                table: "Goals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GoalScoreBoost_Goals_GoalId",
                table: "GoalScoreBoost",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InterestAreaScoreBoost_InterestAreas_InterestAreaId",
                table: "InterestAreaScoreBoost",
                column: "InterestAreaId",
                principalTable: "InterestAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP EXTENSION citext;");
            
            migrationBuilder.DropForeignKey(
                name: "FK_GoalScoreBoost_Goals_GoalId",
                table: "GoalScoreBoost");

            migrationBuilder.DropForeignKey(
                name: "FK_InterestAreaScoreBoost_InterestAreas_InterestAreaId",
                table: "InterestAreaScoreBoost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InterestAreas",
                table: "InterestAreas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Goals",
                table: "Goals");

            migrationBuilder.RenameTable(
                name: "InterestAreas",
                newName: "InterestArea");

            migrationBuilder.RenameTable(
                name: "Goals",
                newName: "Goal");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:citext", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InterestArea",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "citext");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Goal",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "citext");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InterestArea",
                table: "InterestArea",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Goal",
                table: "Goal",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GoalScoreBoost_Goal_GoalId",
                table: "GoalScoreBoost",
                column: "GoalId",
                principalTable: "Goal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InterestAreaScoreBoost_InterestArea_InterestAreaId",
                table: "InterestAreaScoreBoost",
                column: "InterestAreaId",
                principalTable: "InterestArea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
