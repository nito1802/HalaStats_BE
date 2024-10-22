using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HalaStats_BE.Database.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "HalaStats");

            migrationBuilder.CreateTable(
                name: "MatchSchedules",
                schema: "HalaStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SkarbnikId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    TeamA_TeamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamA_Goals = table.Column<int>(type: "int", nullable: true),
                    TeamA_TeamRating = table.Column<int>(type: "int", nullable: true),
                    TeamA_Handicup = table.Column<int>(type: "int", nullable: true),
                    TeamA_HandicupReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamB_TeamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamB_Goals = table.Column<int>(type: "int", nullable: true),
                    TeamB_TeamRating = table.Column<int>(type: "int", nullable: true),
                    TeamB_Handicup = table.Column<int>(type: "int", nullable: true),
                    TeamB_HandicupReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchSchedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                schema: "HalaStats",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatchIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                schema: "HalaStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamA_TeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamA_Goals = table.Column<int>(type: "int", nullable: false),
                    TeamA_TeamRating = table.Column<int>(type: "int", nullable: false),
                    TeamA_Handicup = table.Column<int>(type: "int", nullable: false),
                    TeamA_HandicupReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamB_TeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamB_Goals = table.Column<int>(type: "int", nullable: false),
                    TeamB_TeamRating = table.Column<int>(type: "int", nullable: false),
                    TeamB_Handicup = table.Column<int>(type: "int", nullable: false),
                    TeamB_HandicupReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkarbnikId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatchScheduleId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_MatchSchedules_MatchScheduleId",
                        column: x => x.MatchScheduleId,
                        principalSchema: "HalaStats",
                        principalTable: "MatchSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "EloRatingEntity",
                schema: "HalaStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    MatchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlayerEntityId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EloRatingEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EloRatingEntity_Players_PlayerEntityId",
                        column: x => x.PlayerEntityId,
                        principalSchema: "HalaStats",
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Matches_TeamA_Players",
                schema: "HalaStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamA_MatchId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Difference = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches_TeamA_Players", x => new { x.TeamA_MatchId, x.Id });
                    table.ForeignKey(
                        name: "FK_Matches_TeamA_Players_Matches_TeamA_MatchId",
                        column: x => x.TeamA_MatchId,
                        principalSchema: "HalaStats",
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matches_TeamB_Players",
                schema: "HalaStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamB_MatchId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Difference = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches_TeamB_Players", x => new { x.TeamB_MatchId, x.Id });
                    table.ForeignKey(
                        name: "FK_Matches_TeamB_Players_Matches_TeamB_MatchId",
                        column: x => x.TeamB_MatchId,
                        principalSchema: "HalaStats",
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EloRatingEntity_PlayerEntityId",
                schema: "HalaStats",
                table: "EloRatingEntity",
                column: "PlayerEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_MatchScheduleId",
                schema: "HalaStats",
                table: "Matches",
                column: "MatchScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EloRatingEntity",
                schema: "HalaStats");

            migrationBuilder.DropTable(
                name: "Matches_TeamA_Players",
                schema: "HalaStats");

            migrationBuilder.DropTable(
                name: "Matches_TeamB_Players",
                schema: "HalaStats");

            migrationBuilder.DropTable(
                name: "MatchSchedules_TeamA_Players",
                schema: "HalaStats");

            migrationBuilder.DropTable(
                name: "MatchSchedules_TeamB_Players",
                schema: "HalaStats");

            migrationBuilder.DropTable(
                name: "Players",
                schema: "HalaStats");

            migrationBuilder.DropTable(
                name: "Matches",
                schema: "HalaStats");

            migrationBuilder.DropTable(
                name: "MatchSchedules",
                schema: "HalaStats");
        }
    }
}
