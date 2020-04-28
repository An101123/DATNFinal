using Microsoft.EntityFrameworkCore;
using ScientificResearch.Core.Entities;
using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.DataAccess
{
    public class ScientificResearchDbContext : DbContext
    {
        public ScientificResearchDbContext(DbContextOptions<ScientificResearchDbContext> options) : base(options)
        {
        }
        public DbSet<Lecturer> Lecturers { get; set; }

        public DbSet<ScientificWork> ScientificWorks { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<ScientificReport> ScientificReports { get; set; }
        public DbSet<ScientificReportType> ScientificReportTypes { get; set; }
        public DbSet<News> Newss { get; set; }
        public DbSet<LevelStudyGuide> LevelStudyGuides { get; set; }
        public DbSet<StudyGuide> StudyGuides { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<PublishBook> PublishBooks { get; set; }
        public DbSet<ClassificationOfScientificWork> ClassificationOfScientificWorks { get; set; }
        public DbSet<OtherScientificWork> OtherScientificWorks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LecturerInScientificWork> LecturerInScientificWorks { get; set; }
        public DbSet<LecturerInScientificReport> LecturerInScientificReports { get; set; }
        public DbSet<LecturerInPublishBook> LecturerInPublishBooks { get; set; }
        public DbSet<LecturerInOtherScientificWork> LecturerInOtherScientificWorks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LecturerInScientificWork>().HasKey(t => new { t.LecturerId, t.ScientificWorkId });
            modelBuilder.Entity<LecturerInScientificWork>()
                .HasOne(pt => pt.Lecturer)
                .WithMany(p => p.LecturerInScientificWorks)
                .HasForeignKey(pt => pt.LecturerId);
            modelBuilder.Entity<LecturerInScientificWork>()
                .HasOne(pt => pt.ScientificWork)
                .WithMany(p => p.LecturerInScientificWorks)
                .HasForeignKey(pt => pt.ScientificWorkId);

            modelBuilder.Entity<LecturerInScientificReport>().HasKey(t => new { t.LecturerId, t.ScientificReportId });
            modelBuilder.Entity<LecturerInScientificReport>()
                .HasOne(pt => pt.Lecturer)
                .WithMany(p => p.LecturerInScientificReports)
                .HasForeignKey(pt => pt.LecturerId);
            modelBuilder.Entity<LecturerInScientificReport>()
                .HasOne(pt => pt.ScientificReport)
                .WithMany(p => p.LecturerInScientificReports)
                .HasForeignKey(pt => pt.ScientificReportId);

            modelBuilder.Entity<LecturerInPublishBook>().HasKey(t => new { t.LecturerId, t.PublishBookId });
            modelBuilder.Entity<LecturerInPublishBook>()
                .HasOne(pt => pt.Lecturer)
                .WithMany(p => p.LecturerInPublishBooks)
                .HasForeignKey(pt => pt.LecturerId);
            modelBuilder.Entity<LecturerInPublishBook>()
                .HasOne(pt => pt.PublishBook)
                .WithMany(p => p.LecturerInPublishBooks)
                .HasForeignKey(pt => pt.PublishBookId);

            modelBuilder.Entity<LecturerInOtherScientificWork>().HasKey(t => new { t.LecturerId, t.OtherScientificWorkId });
            modelBuilder.Entity<LecturerInOtherScientificWork>()
                .HasOne(pt => pt.Lecturer)
                .WithMany(p => p.LecturerInOtherScientificWorks)
                .HasForeignKey(pt => pt.LecturerId);
            modelBuilder.Entity<LecturerInOtherScientificWork>()
                .HasOne(pt => pt.OtherScientificWork)
                .WithMany(p => p.LecturerInOtherScientificWorks)
                .HasForeignKey(pt => pt.OtherScientificWorkId);
        }
    }
}
