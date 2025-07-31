using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThriveKid.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSleepLogsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Milestones",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Milestones",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "FeedingLogs",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "SleepLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SleepStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SleepEnd = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: false),
                    ChildId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SleepLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SleepLogs_Children_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Children",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SleepLogs_ChildId",
                table: "SleepLogs",
                column: "ChildId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SleepLogs");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Milestones");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Milestones",
                newName: "Description");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "FeedingLogs",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
