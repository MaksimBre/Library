using Library.BusinessLogicLayer.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.PresentationLayer.Web.Models
{
    public class GenreModel
    {
        public GenreModel() { }
        public GenreModel(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Genre name must be between 3 and 50 characters.")]
        [DisplayName("Genre name")]
        [Required]
        public string Name { get; set; }

        public bool IsChecked { get; set; }

        public static implicit operator Genre(GenreModel gm)
        {
            Genre genre = new Genre(gm.Name)
            {
                Id = gm.Id
            };

            return genre;
        }

        public static implicit operator GenreModel(Genre g)
        {
            GenreModel genre = new GenreModel(g.Name)
            {
                Id = g.Id
            };

            return genre;
        }
    }
}