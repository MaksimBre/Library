using System;
using System.Diagnostics;

namespace Library.BusinessLogicLayer.Models
{
    public class Book
    {
        private string title;

        public Book() { }
        public Book(string title, int noOfPages, int stockCount, Writer writer)
        {
            Title = title;
            NoOfPages = noOfPages;
            StockCount = stockCount;
            Writer = writer;
        }

        public string Title
        {
            get
            {
                Debug.Assert(title != null);
                return title;
            }

            set
            {
                if (value == null)
                    throw new ArgumentNullException("title", "Valid title is mandatory!");

                string oldValue = title;
                try
                {
                    title = value;
                }
                catch
                {
                    title = oldValue;
                }
            }
        }

        public int Id { get; set; }
        public int NoOfPages { get; set; }
        public int StockCount { get; set; }
        public Writer Writer { get; set; }
    }
}
