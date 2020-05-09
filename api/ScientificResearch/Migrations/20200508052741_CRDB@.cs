using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScientificResearch.Migrations
{
    public partial class CRDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "StudyGuides",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "StudyGuides",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "StudyGuides",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "ScientificWorks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "ScientificWorks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "ScientificWorks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "ScientificReportTypes",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "ScientificReportTypes",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "ScientificReportTypes",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "ScientificReports",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "ScientificReports",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "ScientificReports",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "PublishBooks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "PublishBooks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "PublishBooks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "OtherScientificWorks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "OtherScientificWorks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "OtherScientificWorks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "Newss",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Newss",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "Newss",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "LevelStudyGuide",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "LevelStudyGuide",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "LevelStudyGuide",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "Level",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Level",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "Level",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "Lecturers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Lecturers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "Lecturers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "LecturerInScientificWork",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "LecturerInScientificWork",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "LecturerInScientificWork",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "LecturerInScientificReport",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "LecturerInScientificReport",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "LecturerInScientificReport",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "LecturerInPublishBook",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "LecturerInPublishBook",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "LecturerInPublishBook",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "LecturerInOtherScientificWork",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "LecturerInOtherScientificWork",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "LecturerInOtherScientificWork",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "ClassificationOfScientificWork",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "ClassificationOfScientificWork",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "ClassificationOfScientificWork",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "BookCategory",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "BookCategory",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "BookCategory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "StudyGuides");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "StudyGuides");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "StudyGuides");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "ScientificWorks");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ScientificWorks");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ScientificWorks");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "ScientificReportTypes");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ScientificReportTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ScientificReportTypes");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "ScientificReports");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ScientificReports");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ScientificReports");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "PublishBooks");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PublishBooks");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "PublishBooks");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "OtherScientificWorks");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "OtherScientificWorks");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "OtherScientificWorks");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Newss");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Newss");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Newss");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "LevelStudyGuide");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "LevelStudyGuide");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "LevelStudyGuide");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Level");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Level");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Level");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "LecturerInScientificWork");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "LecturerInScientificWork");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "LecturerInScientificWork");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "LecturerInScientificReport");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "LecturerInScientificReport");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "LecturerInScientificReport");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "LecturerInPublishBook");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "LecturerInPublishBook");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "LecturerInPublishBook");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "LecturerInOtherScientificWork");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "LecturerInOtherScientificWork");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "LecturerInOtherScientificWork");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "ClassificationOfScientificWork");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ClassificationOfScientificWork");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ClassificationOfScientificWork");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "BookCategory");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "BookCategory");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "BookCategory");
        }
    }
}
