using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HalaStats_BE.Database.Migrations
{
    /// <inheritdoc />
    public partial class matchSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_SkarbnikId",
                schema: "HalaStats",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_SkarbnikId",
                schema: "HalaStats",
                table: "Matches");

            migrationBuilder.AlterColumn<string>(
                name: "SkarbnikId",
                schema: "HalaStats",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "MatchSchedules",
                schema: "HalaStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SkarbnikId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchSchedules", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchSchedules",
                schema: "HalaStats");

            migrationBuilder.AlterColumn<string>(
                name: "SkarbnikId",
                schema: "HalaStats",
                table: "Matches",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_SkarbnikId",
                schema: "HalaStats",
                table: "Matches",
                column: "SkarbnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_SkarbnikId",
                schema: "HalaStats",
                table: "Matches",
                column: "SkarbnikId",
                principalSchema: "HalaStats",
                principalTable: "Players",
                principalColumn: "Id");
        }
    }
}
