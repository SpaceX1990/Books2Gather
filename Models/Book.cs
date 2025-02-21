using System;
using System.Collections.Generic;
using System.Linq;

namespace Books2Gather.Models {
    public class Book {
        public string Title { get; set; }
        public List<Author> AuthorList { get; set; } = new List<Author>();
        public List<Genre> GenreList { get; set; } = new List<Genre>();
        public string ISBN { get; set; }
        public DateOnly PublishingDate { get; set; }
        public decimal Prize { get; set; }
        public string AuthorNames => string.Join(", ", AuthorList.Select(a => $"{a.Firstname} {a.Lastname}"));
        public string GenreNames => string.Join(", ", GenreList.Select(g => g.Description));
    }
}
