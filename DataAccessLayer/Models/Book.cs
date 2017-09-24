namespace Library.DataAccessLayer.Models
{
    public class Book
    {
        public Book() { }
        public Book(int id, string title, int noOfPages, int stockCount, int writerId)
        {
            Id = id;
            Title = title;
            NoOfPages = noOfPages;
            StockCount = stockCount;
            WriterId = writerId;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int NoOfPages { get; set; }
        public int StockCount { get; set; }
        public int WriterId { get; set; }
    }
}
