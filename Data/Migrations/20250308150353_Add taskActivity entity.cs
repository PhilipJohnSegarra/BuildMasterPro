using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildMasterPro.Migrations
{
    /// <inheritdoc />
    public partial class AddtaskActivityentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagesMongoID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: true),
                    ActivityUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskActivities_AspNetUsers_ActivityUserId",
                        column: x => x.ActivityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskActivities_ProjectTask_TaskId",
                        column: x => x.TaskId,
                        principalTable: "ProjectTask",
                        principalColumn: "TaskId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskActivities_ActivityUserId",
                table: "TaskActivities",
                column: "ActivityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskActivities_TaskId",
                table: "TaskActivities",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskActivities");
        }
    }
}
