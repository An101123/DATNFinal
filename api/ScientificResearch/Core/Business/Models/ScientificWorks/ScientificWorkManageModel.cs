using ScientificResearch.Core.Business.IoC;
using ScientificResearch.Core.DataAccess.Repository.Base;
using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Models.ScientificWorks
{
    public class ScientificWorkManageModel : IValidatableObject
    {
        public string Name { get; set; }

        public string Content { get; set; }

        public Guid LevelId { get; set; }

        public List<Guid> LecturerIds { get; set; }

        public DateTime Time { get; set; }

        //public Guid UserId { get; set; }

        public void GetScientificWorkFromModel(ScientificWork scientificWork)
        {
            scientificWork.Name = Name;
            scientificWork.Content = Content;
            scientificWork.LevelId = LevelId;
            scientificWork.Time = Time;
            //scientificWork.UserId = UserId;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validation)
        {
            if (string.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult("Name is required!", new string[] { "Name" });
            }
            if (LevelId == Guid.Empty)
            {
                yield return new ValidationResult("Level is required!", new string[] { "LevelId" });
            }
            var levelRepository = IoCHelper.GetInstance<IRepository<Level>>();
            var level = levelRepository.GetAll().FirstOrDefault(x => x.Id == LevelId);
            if (level == null) 
            {
                yield return new ValidationResult("Level is not found!", new string[] { "LevelId" });
            }

            var lecturerRepository = IoCHelper.GetInstance<IRepository<Lecturer>>();
            if (LecturerIds.Count <= 0)
            {
                yield return new ValidationResult("Lecturer is required!", new string[] { "LecturerIds" });
            }
            foreach (var lecturerId in LecturerIds)
            {
                var lecturer = lecturerRepository.GetAll().FirstOrDefault(x => x.Id == lecturerId);
                if (lecturer == null)
                {
                    yield return new ValidationResult("Lecturer is not found!", new string[] { "lecturerId" });
                }
            }
        }

    }
}
