using ScientificResearch.Core.Business.IoC;
using ScientificResearch.Core.DataAccess.Repository.Base;
using ScientificResearch.Core.Entities;
using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Models.OtherScientificWorks
{
    public class OtherScientificWorkManageModel : IValidatableObject
    {
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public Guid ClassificationOfScientificWorkId { get; set; }
        public Guid LecturerId { get; set; }


        //public Guid UserId { get; set; }

        public void GetOtherScientificWorkFromModel(OtherScientificWork otherScientificWork)
        {
            otherScientificWork.Name = Name; 
            otherScientificWork.Time = Time;
            otherScientificWork.ClassificationOfScientificWorkId = ClassificationOfScientificWorkId;
            otherScientificWork.LecturerId = LecturerId;
            //scientificWork.UserId = UserId;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validation)
        {
            if (string.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult("Name is required!", new string[] { "Name" });
            }
            if (ClassificationOfScientificWorkId == Guid.Empty)
            {
                yield return new ValidationResult("ClassificationOfScientificWork is required!", new string[] { "ClassificationOfScientificWorkId" });
            }
            var levelRepository = IoCHelper.GetInstance<IRepository<ClassificationOfScientificWork>>();
            var level = levelRepository.GetAll().FirstOrDefault(x => x.Id == ClassificationOfScientificWorkId);
            if (level == null)
            {
                yield return new ValidationResult("ClassificationOfScientificWork is not found!", new string[] { "ClassificationOfScientificWorkId" });
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
