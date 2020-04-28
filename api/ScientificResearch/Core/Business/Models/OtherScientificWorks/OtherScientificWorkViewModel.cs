using ScientificResearch.Core.Business.Models.ClassificationOfScientificWorks;
using ScientificResearch.Core.Business.Models.Lecturers;
using ScientificResearch.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Models.OtherScientificWorks
{
    public class OtherScientificWorkViewModel
    {
        public OtherScientificWorkViewModel()
        {

        }

        public OtherScientificWorkViewModel(OtherScientificWork otherScientificWork) : this()
        {
            if (otherScientificWork != null)
            {
                Id = otherScientificWork.Id;
                Name = otherScientificWork.Name;
               
                Time = otherScientificWork.Time;
                ClassificationOfScientificWork = new ClassificationOfScientificWorkViewModel(otherScientificWork.ClassificationOfScientificWork);
                Lecturers = GetLecturer(otherScientificWork);
                //User = new UserViewModel(scientificWork.User);
                //Lecturer = new LecturerViewModel;
            }
        }
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime Time { get; set; }
        public ClassificationOfScientificWorkViewModel ClassificationOfScientificWork { get; }

        public List<LecturerViewModel> Lecturers { get; set; }

        private List<LecturerViewModel> GetLecturer(OtherScientificWork otherScientificWork)
        {
            var lecturers = new List<LecturerViewModel>();
            foreach (var lecturerInOtherScientificWork in otherScientificWork.LecturerInOtherScientificWorks)
            {
                lecturers.Add(new LecturerViewModel(lecturerInOtherScientificWork.Lecturer));
            }
            return lecturers;
        }
        //public UserViewModel User { get; set; }
    }
}
