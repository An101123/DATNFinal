using ScientificResearch.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Models.LevelStudyGuides
{
    
    public class LevelStudyGuideManageModel : IValidatableObject
    {
        public string Name { get; set; }

        public float Score { get; set; }

        public int HoursConverted { get; set; }

        public void GetLevelStudyGuideFromModel(LevelStudyGuide levelStudyGuide)
        {
            levelStudyGuide.Name = Name;
            levelStudyGuide.Score = Score;
            levelStudyGuide.HoursConverted = HoursConverted;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult("LevelStudyGuide name is required!", new string[] { "Name" });
            }
        }
    }
}
