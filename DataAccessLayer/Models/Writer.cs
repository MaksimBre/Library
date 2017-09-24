namespace Library.DataAccessLayer.Models
{
    public class Writer
    {
        public Writer() { }
        public Writer(int id, string name, string bio, int? noOfBooks = null)
        {
            Id = id;
            Name = name;
            Biography = bio;
            NoOfBooks = noOfBooks;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public int? NoOfBooks { get; set; }
    }
}
