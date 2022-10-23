namespace IteratorsAndComparators
{
    using System.Collections.Generic;

    public class Library
    {
        public Library(params Book[] books)
        {
            Books = new List<Book>(books);
        }

        public List<Book> Books { get; set; }
    }
}
