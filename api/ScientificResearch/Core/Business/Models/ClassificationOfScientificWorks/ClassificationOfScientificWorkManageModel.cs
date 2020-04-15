using ScientificResearch.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Models.ClassificationOfScientificWorks
{
    public class ClassificationOfScientificWorkManageModel : IValidatableObject
    {
        public string Name { get; set; }

        public float Score { get; set; }
        public int HoursConverted { get; set; }

        public void GetClassificationOfScientificWorkFromModel(ClassificationOfScientificWork classificationOfScientificWork)
        {
            classificationOfScientificWork.Name = Name;
            classificationOfScientificWork.Score = Score;
            classificationOfScientificWork.HoursConverted = HoursConverted;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult("classificationOfScientificWork name is required!", new string[] { "Name" });
            }
        }
    }
}
