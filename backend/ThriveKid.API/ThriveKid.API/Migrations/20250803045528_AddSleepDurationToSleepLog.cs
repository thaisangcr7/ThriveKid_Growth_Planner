using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThriveKid.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSleepDurationToSleepLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SleepStart",
                table: "SleepLogs",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "SleepEnd",
                table: "SleepLogs",
                newName: "EndTime");

            migrationBuilder.AddColumn<double>(
                name: "SleepDurationHours",
                table: "SleepLogs",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SleepDurationHours",
                table: "SleepLogs");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "SleepLogs",
                newName: "SleepStart");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "SleepLogs",
                newName: "SleepEnd");
        }
    }
}
