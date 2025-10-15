using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SymphonyLimited.Migrations
{
    /// <inheritdoc />
    public partial class ExamDateTimeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ExamDate",
                table: "Exams",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "ExamTime",
                table: "Exams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExamTime",
                table: "Exams");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExamDate",
                table: "Exams",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
