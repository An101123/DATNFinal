using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScientificResearch.Migrations
{
    public partial class CRDnn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassificationOfScientificWork",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RecordOrder = table.Column<int>(nullable: false),
                    RecordDeleted = table.Column<bool>(nullable: false),
                    RecordActive = table.Column<bool>(nullable: false),
                    CreateOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Score = table.Column<float>(nullable: false),
                    HoursConverted = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassificationOfScientificWork", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OtherScientificWorks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RecordOrder = table.Column<int>(nullable: false),
                    RecordDeleted = table.Column<bool>(nullable: false),
                    RecordActive = table.Column<bool>(nullable: false),
                    CreateOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    ClassificationOfScientificWorkId = table.Column<Guid>(nullable: false),
                    LecturerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherScientificWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtherScientificWorks_ClassificationOfScientificWork_ClassificationOfScientificWorkId",
                        column: x => x.ClassificationOfScientificWorkId,
                        principalTable: "ClassificationOfScientificWork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OtherScientificWorks_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OtherScientificWorks_ClassificationOfScientificWorkId",
                table: "OtherScientificWorks",
                column: "ClassificationOfScientificWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherScientificWorks_LecturerId",
                table: "OtherScientificWorks",
                column: "LecturerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtherScientificWorks");

            migrationBuilder.DropTable(
                name: "ClassificationOfScientificWork");
        }
    }
}
