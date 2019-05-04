using System;
using System.Collections.Generic;

namespace BookStoreApp.Models
{
    /// <see cref="BookAuthors"/> navigation property of <see cref="Author"/> class 
    /// will be lazy-loaded. Check additional information for this lazy-loading
    /// in the <see cref="BookStoreContext"/> class.
    public partial class Author
    {
        /// <see cref="Author"/> constructor disabled to enable lazy-loading
        //public Author()
        //{
        //    BookAuthors = new HashSet<BookAuthors>();
        //}

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual AuthorContact AuthorContact { get; set; }
        public virtual ICollection<BookAuthors> BookAuthors { get; set; }
    }
}
