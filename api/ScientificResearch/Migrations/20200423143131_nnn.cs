using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScientificResearch.Migrations
{
    public partial class nnn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OtherScientificWorks_Lecturers_LecturerId",
                table: "OtherScientificWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_PublishBooks_Lecturers_LecturerId",
                table: "PublishBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_ScientificReports_Lecturers_LecturerId",
                table: "ScientificReports");

            migrationBuilder.DropIndex(
                name: "IX_ScientificReports_LecturerId",
                table: "ScientificReports");

            migrationBuilder.DropIndex(
                name: "IX_PublishBooks_LecturerId",
                table: "PublishBooks");

            migrationBuilder.DropIndex(
                name: "IX_OtherScientificWorks_LecturerId",
                table: "OtherScientificWorks");

            migrationBuilder.DropColumn(
                name: "LecturerId",
                table: "ScientificReports");

            migrationBuilder.DropColumn(
                name: "LecturerId",
                table: "PublishBooks");

            migrationBuilder.DropColumn(
                name: "LecturerId",
                table: "OtherScientificWorks");

            migrationBuilder.DropColumn(
                name: "OtherScientificWorkId",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "PublishBookId",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "ScientificReportId",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "StudyGuideId",
                table: "Lecturers");

            migrationBuilder.CreateTable(
                name: "LecturerInOtherScientificWork",
                columns: table => new
                {
                    LecturerId = table.Column<Guid>(nullable: false),
                    OtherScientificWorkId = table.Column<Guid>(nullable: false),
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
                    table.PrimaryKey("PK_LecturerInOtherScientificWork", x => new { x.LecturerId, x.OtherScientificWorkId });
                    table.ForeignKey(
                        name: "FK_LecturerInOtherScientificWork_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LecturerInOtherScientificWork_OtherScientificWorks_OtherScientificWorkId",
                        column: x => x.OtherScientificWorkId,
                        principalTable: "OtherScientificWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LecturerInPublishBook",
                columns: table => new
                {
                    LecturerId = table.Column<Guid>(nullable: false),
                    PublishBookId = table.Column<Guid>(nullable: false),
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
                    table.PrimaryKey("PK_LecturerInPublishBook", x => new { x.LecturerId, x.PublishBookId });
                    table.ForeignKey(
                        name: "FK_LecturerInPublishBook_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LecturerInPublishBook_PublishBooks_PublishBookId",
                        column: x => x.PublishBookId,
                        principalTable: "PublishBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LecturerInScientificReport",
                columns: table => new
                {
                    LecturerId = table.Column<Guid>(nullable: false),
                    ScientificReportId = table.Column<Guid>(nullable: false),
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
                    table.PrimaryKey("PK_LecturerInScientificReport", x => new { x.LecturerId, x.ScientificReportId });
                    table.ForeignKey(
                        name: "FK_LecturerInScientificReport_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LecturerInScientificReport_ScientificReports_ScientificReportId",
                        column: x => x.ScientificReportId,
                        principalTable: "ScientificReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LecturerInOtherScientificWork_OtherScientificWorkId",
                table: "LecturerInOtherScientificWork",
                column: "OtherScientificWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_LecturerInPublishBook_PublishBookId",
                table: "LecturerInPublishBook",
                column: "PublishBookId");

            migrationBuilder.CreateIndex(
                name: "IX_LecturerInScientificReport_ScientificReportId",
                table: "LecturerInScientificReport",
                column: "ScientificReportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LecturerInOtherScientificWork");

            migrationBuilder.DropTable(
                name: "LecturerInPublishBook");

            migrationBuilder.DropTable(
                name: "LecturerInScientificReport");

            migrationBuilder.AddColumn<Guid>(
                name: "LecturerId",
                table: "ScientificReports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LecturerId",
                table: "PublishBooks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LecturerId",
                table: "OtherScientificWorks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OtherScientificWorkId",
                table: "Lecturers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PublishBookId",
                table: "Lecturers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ScientificReportId",
                table: "Lecturers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StudyGuideId",
                table: "Lecturers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ScientificReports_LecturerId",
                table: "ScientificReports",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_PublishBooks_LecturerId",
                table: "PublishBooks",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherScientificWorks_LecturerId",
                table: "OtherScientificWorks",
                column: "LecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherScientificWorks_Lecturers_LecturerId",
                table: "OtherScientificWorks",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublishBooks_Lecturers_LecturerId",
                table: "PublishBooks",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScientificReports_Lecturers_LecturerId",
                table: "ScientificReports",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
