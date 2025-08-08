using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThriveKid.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReminderForEngine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReminderTime",
                table: "Reminders",
                newName: "DueAt");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Reminders",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastRunAt",
                table: "Reminders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NextRunAt",
                table: "Reminders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RepeatRule",
                table: "Reminders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Source",
                table: "Reminders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "LastRunAt",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "NextRunAt",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "RepeatRule",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "Reminders");

            migrationBuilder.RenameColumn(
                name: "DueAt",
                table: "Reminders",
                newName: "ReminderTime");
        }
    }
}
