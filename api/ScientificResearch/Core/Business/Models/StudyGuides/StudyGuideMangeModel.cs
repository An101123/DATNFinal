using ScientificResearch.Core.Business.IoC;
using ScientificResearch.Core.DataAccess.Repository.Base;
using ScientificResearch.Core.Entities;
using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Models.StudyGuides
{
    public class StudyGuideManageModel : IValidatableObject
    {
       
        public string Name { get; set; }

        public string Literacy { get; set; }

        public string PlaceOfTraining { get; set; }

        public DateTime InstructionTime { get; set; }

        public DateTime GraduationTime { get; set; }

        public Guid LevelStudyGuideId { get; set; }

        public Guid LecturerId { get; set; }
        public void GetStudyGuideFromModel(StudyGuide studyGuide)
        {
            studyGuide.Name = Name;
            studyGuide.Literacy = Literacy;
            studyGuide.PlaceOfTraining = PlaceOfTraining;
            studyGuide.InstructionTime = InstructionTime;
            studyGuide.GraduationTime = GraduationTime;
            studyGuide.LevelStudyGuideId = LevelStudyGuideId;
            studyGuide.LecturerId = LecturerId;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validation)
        {
            if (string.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult("Name is required!", new string[] { "Name" });
            }
            if (LevelStudyGuideId == Guid.Empty)
            {
                yield return new ValidationResult("LevelStudyGuideId is required!", new string[] { "LevelId" });
            }
            var levelRepository = IoCHelper.GetInstance<IRepository<LevelStudyGuide>>();
            var level = levelRepository.GetAll().FirstOrDefault(x => x.Id == LevelStudyGuideId);
            if (level == null)
            {
                yield return new ValidationResult("LevelStudyGuide is not found!", new string[] { "LevelStudyGuideId" });
            }
            if (LecturerId == Guid.Empty)
            {
                yield return new ValidationResult("Lecturer is required!", new string[] { "LecturerId" });
            }
            var lecturerRepository = IoCHelper.GetInstance<IRepository<Lecturer>>();
            var lecturer = lecturerRepository.GetAll().FirstOrDefault(x => x.Id == LecturerId);
            if (lecturer == null)
            {
                yield return new ValidationResult("Lecturer is not found!");
            }
        }

    }
}
