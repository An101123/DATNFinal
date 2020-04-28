using ScientificResearch.Core.Business.Models.Lecturers;
using ScientificResearch.Core.Business.Models.Levels;
using ScientificResearch.Core.Business.Models.Users;
using ScientificResearch.Core.Entities;
using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Models.ScientificWorks
{
    public class ScientificWorkViewModel
    {
        public ScientificWorkViewModel()
        {

        }

        public ScientificWorkViewModel(ScientificWork scientificWork) : this()
        {
            if (scientificWork != null)
            {
                Id = scientificWork.Id;
                Name = scientificWork.Name;
                Content = scientificWork.Content;
                Time = scientificWork.Time;
                Level = new LevelViewModel(scientificWork.Level);
                Lecturers = GetLecturer(scientificWork);
                //User = new UserViewModel(scientificWork.User);
                //Lecturer = new LecturerViewModel;
            }
        }
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public DateTime Time { get; set; }

        public LevelViewModel Level { get; set; }

        public List<LecturerViewModel> Lecturers { get; set; }

        //public UserViewModel User { get; set; }

        private List<LecturerViewModel> GetLecturer(ScientificWork scientificWork)
        {
            var lecturers = new List<LecturerViewModel>();
            foreach(var lecturerInScientificWork in scientificWork.LecturerInScientificWorks)
            {
                lecturers.Add(new LecturerViewModel(lecturerInScientificWork.Lecturer));
            }
            return lecturers;
        }
    }
}
