using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMTools.Migrations
{
    public partial class tasklogassign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1df37ba-f116-441a-975b-2beb384343b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e0e524bf-7543-43a9-abd1-b921f3a643ac");

            migrationBuilder.CreateTable(
                name: "TaskLogTable",
                columns: table => new
                {
                    TaskLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskLog = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskLogDescriptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskLogTable", x => x.TaskLogId);
                });

            migrationBuilder.CreateTable(
                name: "TaskTable",
                columns: table => new
                {
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeveloperId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TaskDifficulties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTable", x => x.TaskId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "423dabc5-6b0b-43c1-99e3-b362d00b6175", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dc610d50-0025-4f82-96dd-1e6628b9de78", "2", "Developer", "Developer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskLogTable");

            migrationBuilder.DropTable(
                name: "TaskTable");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "423dabc5-6b0b-43c1-99e3-b362d00b6175");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc610d50-0025-4f82-96dd-1e6628b9de78");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c1df37ba-f116-441a-975b-2beb384343b4", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e0e524bf-7543-43a9-abd1-b921f3a643ac", "2", "Developer", "Developer" });
        }
    }
}
