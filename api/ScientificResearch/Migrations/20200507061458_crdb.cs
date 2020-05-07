using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScientificResearch.Migrations
{
    public partial class crdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookCategory",
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
                    table.PrimaryKey("PK_BookCategory", x => x.Id);
                });

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
                name: "Lecturers",
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
                    Faculty = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Total = table.Column<float>(nullable: false),
                    TotalHour = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Level",
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
                    table.PrimaryKey("PK_Level", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LevelStudyGuide",
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
                    table.PrimaryKey("PK_LevelStudyGuide", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Newss",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RecordOrder = table.Column<int>(nullable: false),
                    RecordDeleted = table.Column<bool>(nullable: false),
                    RecordActive = table.Column<bool>(nullable: false),
                    CreateOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScientificReportTypes",
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
                    table.PrimaryKey("PK_ScientificReportTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RecordOrder = table.Column<int>(nullable: false),
                    RecordDeleted = table.Column<bool>(nullable: false),
                    RecordActive = table.Column<bool>(nullable: false),
                    CreateOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(maxLength: 32, nullable: false),
                    AvatarUrl = table.Column<string>(maxLength: 512, nullable: true),
                    FullName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Gender = table.Column<int>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    ResetPasswordExpiryDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublishBooks",
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
                    PlaceOfPublication = table.Column<string>(nullable: true),
                    BookCategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublishBooks_BookCategory_BookCategoryId",
                        column: x => x.BookCategoryId,
                        principalTable: "BookCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    ClassificationOfScientificWorkId = table.Column<Guid>(nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "ScientificWorks",
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
                    Content = table.Column<string>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    LevelId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScientificWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScientificWorks_Level_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Level",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudyGuides",
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
                    Literacy = table.Column<string>(nullable: false),
                    PlaceOfTraining = table.Column<string>(nullable: false),
                    InstructionTime = table.Column<DateTime>(nullable: false),
                    GraduationTime = table.Column<DateTime>(nullable: false),
                    LevelStudyGuideId = table.Column<Guid>(nullable: false),
                    LecturerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyGuides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyGuides_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyGuides_LevelStudyGuide_LevelStudyGuideId",
                        column: x => x.LevelStudyGuideId,
                        principalTable: "LevelStudyGuide",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScientificReports",
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
                    Content = table.Column<string>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    ScientificReportTypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScientificReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScientificReports_ScientificReportTypes_ScientificReportTypeId",
                        column: x => x.ScientificReportTypeId,
                        principalTable: "ScientificReportTypes",
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

            migrationBuilder.CreateIndex(
                name: "IX_LecturerInScientificWork_ScientificWorkId",
                table: "LecturerInScientificWork",
                column: "ScientificWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherScientificWorks_ClassificationOfScientificWorkId",
                table: "OtherScientificWorks",
                column: "ClassificationOfScientificWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_PublishBooks_BookCategoryId",
                table: "PublishBooks",
                column: "BookCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificReports_ScientificReportTypeId",
                table: "ScientificReports",
                column: "ScientificReportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificWorks_LevelId",
                table: "ScientificWorks",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyGuides_LecturerId",
                table: "StudyGuides",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyGuides_LevelStudyGuideId",
                table: "StudyGuides",
                column: "LevelStudyGuideId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LecturerInOtherScientificWork");

            migrationBuilder.DropTable(
                name: "LecturerInPublishBook");

            migrationBuilder.DropTable(
                name: "LecturerInScientificReport");

            migrationBuilder.DropTable(
                name: "LecturerInScientificWork");

            migrationBuilder.DropTable(
                name: "Newss");

            migrationBuilder.DropTable(
                name: "StudyGuides");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "OtherScientificWorks");

            migrationBuilder.DropTable(
                name: "PublishBooks");

            migrationBuilder.DropTable(
                name: "ScientificReports");

            migrationBuilder.DropTable(
                name: "ScientificWorks");

            migrationBuilder.DropTable(
                name: "Lecturers");

            migrationBuilder.DropTable(
                name: "LevelStudyGuide");

            migrationBuilder.DropTable(
                name: "ClassificationOfScientificWork");

            migrationBuilder.DropTable(
                name: "BookCategory");

            migrationBuilder.DropTable(
                name: "ScientificReportTypes");

            migrationBuilder.DropTable(
                name: "Level");
        }
    }
}
