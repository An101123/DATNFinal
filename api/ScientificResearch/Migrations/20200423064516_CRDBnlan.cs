using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScientificResearch.Migrations
{
    public partial class CRDBnlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScientificWorks_Lecturers_LecturerId",
                table: "ScientificWorks");

            migrationBuilder.DropIndex(
                name: "IX_ScientificWorks_LecturerId",
                table: "ScientificWorks");

            migrationBuilder.DropColumn(
                name: "LecturerId",
                table: "ScientificWorks");

            migrationBuilder.DropColumn(
                name: "ScientificWorkId",
                table: "Lecturers");

            migrationBuilder.CreateTable(
                name: "LecturerInScientificWork",
                columns: table => new
                {
                    LecturerId = table.Column<Guid>(nullable: false),
                    ScientificWorkId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    RecordOrder = table.Column<int>(nullable: false),
                    RecordDeleted = table.Column<bool>(nullable: false),
                    RecordActive = table.Column<bool>(nullable: false),
                    CreateOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturerInScientificWork", x => new { x.LecturerId, x.ScientificWorkId });
                    table.ForeignKey(
                        name: "FK_LecturerInScientificWork_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LecturerInScientificWork_ScientificWorks_ScientificWorkId",
                        column: x => x.ScientificWorkId,
                        principalTable: "ScientificWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LecturerInScientificWork_ScientificWorkId",
                table: "LecturerInScientificWork",
                column: "ScientificWorkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LecturerInScientificWork");

            migrationBuilder.AddColumn<Guid>(
                name: "LecturerId",
                table: "ScientificWorks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ScientificWorkId",
                table: "Lecturers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ScientificWorks_LecturerId",
                table: "ScientificWorks",
                column: "LecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScientificWorks_Lecturers_LecturerId",
                table: "ScientificWorks",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
