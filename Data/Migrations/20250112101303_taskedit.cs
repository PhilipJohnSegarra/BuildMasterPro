using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildMasterPro.Migrations
{
    /// <inheritdoc />
    public partial class taskedit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ActualEndDate",
                table: "ProjectTask",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualStartDate",
                table: "ProjectTask",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ProjectTask",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "ProjectTask",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PlannedEndDate",
                table: "ProjectTask",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PlannedStartDate",
                table: "ProjectTask",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "TaskCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTask_CategoryId",
                table: "ProjectTask",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTask_TaskCategory_CategoryId",
                table: "ProjectTask",
                column: "CategoryId",
                principalTable: "TaskCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTask_TaskCategory_CategoryId",
                table: "ProjectTask");

            migrationBuilder.DropTable(
                name: "TaskCategory");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTask_CategoryId",
                table: "ProjectTask");

            migrationBuilder.DropColumn(
                name: "ActualEndDate",
                table: "ProjectTask");

            migrationBuilder.DropColumn(
                name: "ActualStartDate",
                table: "ProjectTask");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ProjectTask");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "ProjectTask");

            migrationBuilder.DropColumn(
                name: "PlannedEndDate",
                table: "ProjectTask");

            migrationBuilder.DropColumn(
                name: "PlannedStartDate",
                table: "ProjectTask");
        }
    }
}
