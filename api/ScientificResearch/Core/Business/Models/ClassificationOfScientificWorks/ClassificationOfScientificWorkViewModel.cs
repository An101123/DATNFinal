using ScientificResearch.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Models.ClassificationOfScientificWorks
{
   
    public class ClassificationOfScientificWorkViewModel
    {
        public ClassificationOfScientificWorkViewModel()
        {

        }

        public ClassificationOfScientificWorkViewModel(ClassificationOfScientificWork classificationOfScientificWork) : this()
        {
            if (classificationOfScientificWork != null)
            {
                Id = classificationOfScientificWork.Id;
                Name = classificationOfScientificWork.Name;
                Score = classificationOfScientificWork.Score;
                HoursConverted = classificationOfScientificWork.HoursConverted;
            }
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public float Score { get; set; }

        public int HoursConverted { get; set; }

    }
}
