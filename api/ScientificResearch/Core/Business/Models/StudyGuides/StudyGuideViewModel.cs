using ScientificResearch.Core.Business.Models.Lecturers;
using ScientificResearch.Core.Business.Models.LevelStudyGuides;
using ScientificResearch.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Models.StudyGuides
{
    public class StudyGuideViewModel
    {
        public StudyGuideViewModel()
        {

        }
        public StudyGuideViewModel(StudyGuide studyGuide) : this()
        {
            if (studyGuide != null)
            {
                Id = studyGuide.Id;
                Name = studyGuide.Name;
                Literacy = studyGuide.Literacy;
                PlaceOfTraining = studyGuide.PlaceOfTraining;
                InstructionTime = studyGuide.InstructionTime;
                GraduationTime = studyGuide.GraduationTime;
                LevelStudyGuide = new LevelStudyGuideViewModel(studyGuide.LevelStudyGuide);
                Lecturer = new LecturerViewModel(studyGuide.Lecturer);
                //User = new UserViewModel(scientificWork.User);
                //Lecturer = new LecturerViewModel;
            }
        }
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Literacy { get; set; }

        public string PlaceOfTraining { get; set; }

        public DateTime InstructionTime { get; set; }

        public DateTime GraduationTime { get; set; }

       
        public LevelStudyGuideViewModel LevelStudyGuide { get; set; }

        public LecturerViewModel Lecturer { get; set; }

        //public UserViewModel User { get; set; }
    }
}
