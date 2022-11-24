namespace IteratorsAndComparators
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Library : IEnumerable<Book>
    {
        public Library(params Book[] books)
        {
            Books = new List<Book>(books);
            Books.Sort();
        }

        public List<Book> Books { get; set; }

        public IEnumerator<Book> GetEnumerator()
        {
            return new LibraryIterator(Books);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class LibraryIterator : IEnumerator<Book>
        {
            public LibraryIterator(IEnumerable<Book> books)
            {
                Reset();
                Books = new List<Book>(books);
            }

            public List<Book> Books { get; set; }

            public int CurrentIndex { get; set; }

            public Book Current => Books[CurrentIndex];

            object IEnumerator.Current => Current;


            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                return ++CurrentIndex < Books.Count();
            }

            public void Reset()
            {
                this.CurrentIndex = -1;
            }
        }
    }
}
