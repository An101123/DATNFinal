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

    }
}
