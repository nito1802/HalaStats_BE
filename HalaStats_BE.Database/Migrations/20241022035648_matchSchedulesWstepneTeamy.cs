using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HalaStats_BE.Database.Migrations
{
    /// <inheritdoc />
    public partial class matchSchedulesWstepneTeamy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamA_Goals",
                schema: "HalaStats",
                table: "MatchSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamA_Handicup",
                schema: "HalaStats",
                table: "MatchSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TeamA_HandicupReason",
                schema: "HalaStats",
                table: "MatchSchedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamA_TeamName",
                schema: "HalaStats",
                table: "MatchSchedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TeamA_TeamRating",
                schema: "HalaStats",
                table: "MatchSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamB_Goals",
                schema: "HalaStats",
                table: "MatchSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamB_Handicup",
                schema: "HalaStats",
                table: "MatchSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TeamB_HandicupReason",
                schema: "HalaStats",
                table: "MatchSchedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamB_TeamName",
                schema: "HalaStats",
                table: "MatchSchedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TeamB_TeamRating",
                schema: "HalaStats",
                table: "MatchSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MatchSchedules_TeamA_Players",
                schema: "HalaStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamA_MatchScheduleId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Difference = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchSchedules_TeamA_Players", x => new { x.TeamA_MatchScheduleId, x.Id });
                    table.ForeignKey(
                        name: "FK_MatchSchedules_TeamA_Players_MatchSchedules_TeamA_MatchScheduleId",
                        column: x => x.TeamA_MatchScheduleId,
                        principalSchema: "HalaStats",
                        principalTable: "MatchSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchSchedules_TeamB_Players",
                schema: "HalaStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamB_MatchScheduleId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Difference = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchSchedules_TeamB_Players", x => new { x.TeamB_MatchScheduleId, x.Id });
                    table.ForeignKey(
                        name: "FK_MatchSchedules_TeamB_Players_MatchSchedules_TeamB_MatchScheduleId",
                        column: x => x.TeamB_MatchScheduleId,
                        principalSchema: "HalaStats",
                        principalTable: "MatchSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchSchedules_TeamA_Players",
                schema: "HalaStats");

            migrationBuilder.DropTable(
                name: "MatchSchedules_TeamB_Players",
                schema: "HalaStats");

            migrationBuilder.DropColumn(
                name: "TeamA_Goals",
                schema: "HalaStats",
                table: "MatchSchedules");

            migrationBuilder.DropColumn(
                name: "TeamA_Handicup",
                schema: "HalaStats",
                table: "MatchSchedules");

            migrationBuilder.DropColumn(
                name: "TeamA_HandicupReason",
                schema: "HalaStats",
                table: "MatchSchedules");

            migrationBuilder.DropColumn(
                name: "TeamA_TeamName",
                schema: "HalaStats",
                table: "MatchSchedules");

            migrationBuilder.DropColumn(
                name: "TeamA_TeamRating",
                schema: "HalaStats",
                table: "MatchSchedules");

            migrationBuilder.DropColumn(
                name: "TeamB_Goals",
                schema: "HalaStats",
                table: "MatchSchedules");

            migrationBuilder.DropColumn(
                name: "TeamB_Handicup",
                schema: "HalaStats",
                table: "MatchSchedules");

            migrationBuilder.DropColumn(
                name: "TeamB_HandicupReason",
                schema: "HalaStats",
                table: "MatchSchedules");

            migrationBuilder.DropColumn(
                name: "TeamB_TeamName",
                schema: "HalaStats",
                table: "MatchSchedules");

            migrationBuilder.DropColumn(
                name: "TeamB_TeamRating",
                schema: "HalaStats",
                table: "MatchSchedules");
        }
    }
}
