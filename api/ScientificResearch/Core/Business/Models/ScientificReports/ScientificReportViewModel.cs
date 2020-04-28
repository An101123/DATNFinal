using ScientificResearch.Core.Business.Models.Lecturers;
using ScientificResearch.Core.Business.Models.ScientificReportTypes;
using ScientificResearch.Core.Business.Models.Users;
using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Models.ScientificReports
{
    public class ScientificReportViewModel
    {
        public ScientificReportViewModel()
        {

        }

        public ScientificReportViewModel(ScientificReport scientificReport) : this()
        {
            if (scientificReport != null)
            {
                Id = scientificReport.Id;
                Name = scientificReport.Name;
                Content = scientificReport.Content;
                Time = scientificReport.Time;
                ScientificReportType = new ScientificReportTypeViewModel(scientificReport.ScientificReportType);
                Lecturers = GetLecturer(scientificReport);
                //User = new UserViewModel(scientificReport.User);
            }
        }
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public DateTime Time { get; set; }

        public ScientificReportTypeViewModel ScientificReportType { get; set; }

        public List<LecturerViewModel> Lecturers { get; set; }

        private List<LecturerViewModel> GetLecturer(ScientificReport scientificReport)
        {
            var lecturers = new List<LecturerViewModel>();
            foreach (var lecturerInScientificReport in scientificReport.LecturerInScientificReports)
            {
                lecturers.Add(new LecturerViewModel(lecturerInScientificReport.Lecturer));
            }
            return lecturers;
        }
        //public UserViewModel User { get; set; }

    }
}
