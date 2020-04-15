using ScientificResearch.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Models.BookCategorys
{
   
    public class BookCategoryManageModel : IValidatableObject
    {
        public string Name { get; set; }

        public float Score { get; set; }

        public int HoursConverted { get; set; }
        public void GetBookCategoryFromModel(BookCategory bookCategory)
        {
            bookCategory.Name = Name;
            bookCategory.Score = Score;
            bookCategory.HoursConverted = HoursConverted;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult("BookCategory name is required!", new string[] { "Name" });
            }
        }
    }
}
