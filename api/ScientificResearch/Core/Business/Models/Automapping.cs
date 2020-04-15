using AutoMapper;
using ScientificResearch.Core.Business.Models.BookCategorys;
using ScientificResearch.Core.Business.Models.ClassificationOfScientificWorks;
using ScientificResearch.Core.Business.Models.Lecturers;
using ScientificResearch.Core.Business.Models.Levels;
using ScientificResearch.Core.Business.Models.LevelStudyGuides;
using ScientificResearch.Core.Business.Models.News_s;
using ScientificResearch.Core.Business.Models.OtherScientificWorks;
using ScientificResearch.Core.Business.Models.PublishBooks;
using ScientificResearch.Core.Business.Models.ScientificReports;
using ScientificResearch.Core.Business.Models.ScientificReportTypes;
using ScientificResearch.Core.Business.Models.ScientificWorks;
using ScientificResearch.Core.Business.Models.StudyGuides;
using ScientificResearch.Core.Business.Models.Users;
using ScientificResearch.Core.Entities;
using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Models
{
    public class Automapping : Profile
    {
        public Automapping()
        {
            CreateMap<LevelManageModel, Level>();

            CreateMap<ScientificWorkManageModel, ScientificWork>();

            CreateMap<ScientificReportTypeManageModel, ScientificReportType>();

            CreateMap<ScientificReportManageModel, ScientificReport>();

            CreateMap<LecturerManageModel, Lecturer>();

            CreateMap<UserManageModel, User>();

            CreateMap<NewsManageModel, News>();

            CreateMap<BookCategoryManageModel, BookCategory>();

            CreateMap<PublishBookManageModel, PublishBook>();

            CreateMap<LevelStudyGuideManageModel, LevelStudyGuide>();

            CreateMap<StudyGuideManageModel, StudyGuide>();

            CreateMap<ClassificationOfScientificWorkManageModel, ClassificationOfScientificWork>();

            CreateMap<OtherScientificWorkManageModel, OtherScientificWork>();

            
        }
    }
}
