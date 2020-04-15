using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Models.Levels
{
    public class LevelViewModel
    {
        public LevelViewModel()
        {

        }

        public LevelViewModel(Level level) : this()
        {
            if (level != null)
            {
                Id = level.Id;
                Name = level.Name;
                Score = level.Score;
                HoursConverted = level.HoursConverted;
            }
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public float Score { get; set; }
        public int HoursConverted { get; set; }

    }
}
  
