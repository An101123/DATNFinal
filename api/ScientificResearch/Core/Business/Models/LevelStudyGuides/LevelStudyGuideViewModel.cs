using ScientificResearch.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Models.LevelStudyGuides
{
    
    public class LevelStudyGuideViewModel
    {
        public LevelStudyGuideViewModel()
        {

        }

        public LevelStudyGuideViewModel(LevelStudyGuide levelStudyGuide) : this()
        {
            if (levelStudyGuide != null)
            {
                Id = levelStudyGuide.Id;
                Name = levelStudyGuide.Name;
                Score = levelStudyGuide.Score;
                HoursConverted = levelStudyGuide.HoursConverted;
            }
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public float Score { get; set; }

        public int HoursConverted { get; set; }

    }
}
