﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScientificResearch.Core.DataAccess;

namespace ScientificResearch.Migrations
{
    [DbContext(typeof(ScientificResearchDbContext))]
    [Migration("20200420043700_CRDB7")]
    partial class CRDB7
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ScientificResearch.Core.Entities.BookCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("HoursConverted")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RecordActive")
                        .HasColumnType("bit");

                    b.Property<bool>("RecordDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RecordOrder")
                        .HasColumnType("int");

                    b.Property<float>("Score")
                        .HasColumnType("real");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("BookCategory");
                });

            modelBuilder.Entity("ScientificResearch.Core.Entities.ClassificationOfScientificWork", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("HoursConverted")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RecordActive")
                        .HasColumnType("bit");

                    b.Property<bool>("RecordDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RecordOrder")
                        .HasColumnType("int");

                    b.Property<float>("Score")
                        .HasColumnType("real");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ClassificationOfScientificWork");
                });

            modelBuilder.Entity("ScientificResearch.Core.Entities.LevelStudyGuide", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("HoursConverted")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RecordActive")
                        .HasColumnType("bit");

                    b.Property<bool>("RecordDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RecordOrder")
                        .HasColumnType("int");

                    b.Property<float>("Score")
                        .HasColumnType("real");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("LevelStudyGuide");
                });

            modelBuilder.Entity("ScientificResearch.Core.Entities.OtherScientificWork", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClassificationOfScientificWorkId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LecturerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RecordActive")
                        .HasColumnType("bit");

                    b.Property<bool>("RecordDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RecordOrder")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClassificationOfScientificWorkId");

                    b.HasIndex("LecturerId");

                    b.ToTable("OtherScientificWorks");
                });

            modelBuilder.Entity("ScientificResearch.Core.Entities.PublishBook", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LecturerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaceOfPublication")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RecordActive")
                        .HasColumnType("bit");

                    b.Property<bool>("RecordDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RecordOrder")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BookCategoryId");

                    b.HasIndex("LecturerId");

                    b.ToTable("PublishBooks");
                });

            modelBuilder.Entity("ScientificResearch.Core.Entities.StudyGuide", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("GraduationTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("InstructionTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LecturerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LevelStudyGuideId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Literacy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaceOfTraining")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RecordActive")
                        .HasColumnType("bit");

                    b.Property<bool>("RecordDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RecordOrder")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("LecturerId");

                    b.HasIndex("LevelStudyGuideId");

                    b.ToTable("StudyGuides");
                });

            modelBuilder.Entity("ScientificResearch.Entities.Lecturer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Faculty")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OtherScientificWorkId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PublishBookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("RecordActive")
                        .HasColumnType("bit");

                    b.Property<bool>("RecordDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RecordOrder")
                        .HasColumnType("int");

                    b.Property<Guid>("ScientificReportId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ScientificWorkId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudyGuideId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Total")
                        .HasColumnType("real");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("ScientificResearch.Entities.Level", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("HoursConverted")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RecordActive")
                        .HasColumnType("bit");

                    b.Property<bool>("RecordDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RecordOrder")
                        .HasColumnType("int");

                    b.Property<float>("Score")
                        .HasColumnType("real");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Level");
                });

            modelBuilder.Entity("ScientificResearch.Entities.News", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RecordActive")
                        .HasColumnType("bit");

                    b.Property<bool>("RecordDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RecordOrder")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Newss");
                });

            modelBuilder.Entity("ScientificResearch.Entities.ScientificReport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LecturerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RecordActive")
                        .HasColumnType("bit");

                    b.Property<bool>("RecordDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RecordOrder")
                        .HasColumnType("int");

                    b.Property<Guid>("ScientificReportTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("LecturerId");

                    b.HasIndex("ScientificReportTypeId");

                    b.ToTable("ScientificReports");
                });

            modelBuilder.Entity("ScientificResearch.Entities.ScientificReportType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("HoursConverted")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RecordActive")
                        .HasColumnType("bit");

                    b.Property<bool>("RecordDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RecordOrder")
                        .HasColumnType("int");

                    b.Property<float>("Score")
                        .HasColumnType("real");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ScientificReportTypes");
                });

            modelBuilder.Entity("ScientificResearch.Entities.ScientificWork", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LecturerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LevelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RecordActive")
                        .HasColumnType("bit");

                    b.Property<bool>("RecordDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RecordOrder")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("LecturerId");

                    b.HasIndex("LevelId");

                    b.ToTable("ScientificWorks");
                });

            modelBuilder.Entity("ScientificResearch.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(512)")
                        .HasMaxLength(512);

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<bool>("RecordActive")
                        .HasColumnType("bit");

                    b.Property<bool>("RecordDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RecordOrder")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ResetPasswordExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ScientificResearch.Core.Entities.OtherScientificWork", b =>
                {
                    b.HasOne("ScientificResearch.Core.Entities.ClassificationOfScientificWork", "ClassificationOfScientificWork")
                        .WithMany("OtherScientificWorks")
                        .HasForeignKey("ClassificationOfScientificWorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScientificResearch.Entities.Lecturer", "Lecturer")
                        .WithMany("OtherScientificWorks")
                        .HasForeignKey("LecturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ScientificResearch.Core.Entities.PublishBook", b =>
                {
                    b.HasOne("ScientificResearch.Core.Entities.BookCategory", "BookCategory")
                        .WithMany("PublishBooks")
                        .HasForeignKey("BookCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScientificResearch.Entities.Lecturer", "Lecturer")
                        .WithMany("PublishBooks")
                        .HasForeignKey("LecturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ScientificResearch.Core.Entities.StudyGuide", b =>
                {
                    b.HasOne("ScientificResearch.Entities.Lecturer", "Lecturer")
                        .WithMany("StudyGuides")
                        .HasForeignKey("LecturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScientificResearch.Core.Entities.LevelStudyGuide", "LevelStudyGuide")
                        .WithMany("StudyGuides")
                        .HasForeignKey("LevelStudyGuideId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ScientificResearch.Entities.ScientificReport", b =>
                {
                    b.HasOne("ScientificResearch.Entities.Lecturer", "Lecturer")
                        .WithMany("ScientificReports")
                        .HasForeignKey("LecturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScientificResearch.Entities.ScientificReportType", "ScientificReportType")
                        .WithMany("ScientificReports")
                        .HasForeignKey("ScientificReportTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ScientificResearch.Entities.ScientificWork", b =>
                {
                    b.HasOne("ScientificResearch.Entities.Lecturer", "Lecturer")
                        .WithMany("ScientificWorks")
                        .HasForeignKey("LecturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScientificResearch.Entities.Level", "Level")
                        .WithMany("ScientificWorks")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
