using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildMasterPro.Migrations
{
    /// <inheritdoc />
    public partial class RemovereplacedattributesinTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "ProjectTask");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "ProjectTask");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "ProjectTask",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "ProjectTask",
                type: "datetime2",
                nullable: true);
        }
    }
}
