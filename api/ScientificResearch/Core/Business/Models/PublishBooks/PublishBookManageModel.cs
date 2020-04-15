using ScientificResearch.Core.Business.IoC;
using ScientificResearch.Core.DataAccess.Repository.Base;
using ScientificResearch.Core.Entities;
using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Models.PublishBooks
{
   
    public class PublishBookManageModel : IValidatableObject
    {
        public string Name { get; set; }

        public DateTime Time { get; set; }

        public string PlaceOfPublication { get; set; }
        public Guid BookCategoryId { get; set; }

        public Guid LecturerId { get; set; }

        

        //public Guid UserId { get; set; }

        public void GetPublishBookFromModel(PublishBook publishBook)
        {
            publishBook.Name = Name;
            publishBook.Time = Time;
            publishBook.PlaceOfPublication = PlaceOfPublication;
            publishBook.BookCategoryId = BookCategoryId;
            publishBook.LecturerId = LecturerId;
            //scientificWork.UserId = UserId;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validation)
        {
            if (string.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult("Name is required!", new string[] { "Name" });
            }
            if (BookCategoryId == Guid.Empty)
            {
                yield return new ValidationResult("BookCategory is required!", new string[] { "BookCategoryId" });
            }
            var bookCategoryRepository = IoCHelper.GetInstance<IRepository<BookCategory>>();
            var bookCategory = bookCategoryRepository.GetAll().FirstOrDefault(x => x.Id == BookCategoryId);
            if (bookCategory == null)
            {
                yield return new ValidationResult("BookCategory is not found!", new string[] { "BookCategoryId" });
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
