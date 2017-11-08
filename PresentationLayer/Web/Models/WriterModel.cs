using Library.BusinessLogicLayer.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.PresentationLayer.Web.Models
{
    public class WriterModel
    {
        public WriterModel() { }
        public WriterModel(string name, string bio, int? noOfBooks = null)
        {
            Name = name;
            Biography = bio;
            NoOfBooks = noOfBooks;
        }

        public int Id { get; set; }

        [DisplayName("Writers name")]
        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        [StringLength(300, MinimumLength = 5)]
        public string Biography { get; set; }

        public int? NoOfBooks { get; set; }

        public static implicit operator Writer(WriterModel wm)
        {
            Writer writer = new Writer(wm.Name, wm.Biography, wm.NoOfBooks)
            {
                Id = wm.Id
            };

            return writer;
        }

        public static implicit operator WriterModel(Writer w)
        {
            WriterModel writer = new WriterModel(w.Name, w.Biography, w.NoOfBooks)
            {
                Id = w.Id
            };

            return writer;
        }
    }
}