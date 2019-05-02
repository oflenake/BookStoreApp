using System;
using System.Collections.Generic;

namespace BookStoreApp.Models
{
    public partial class Author
    {
        public Author()
        {
            BookAuthors = new HashSet<BookAuthors>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual AuthorContact AuthorContact { get; set; }
        public virtual ICollection<BookAuthors> BookAuthors { get; set; }
    }
}
