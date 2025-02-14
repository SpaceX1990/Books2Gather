namespace Books2Gather.Models {
    public class Book {
        public string Title { get; set; }

        public List<Author> AuthorList { get; set; }

        public List<Genre> GenreList { get; set; }

        public string ISBN { get; set; }

        public DateOnly PublishingDate { get; set; }

        public Decimal Prize { get; set; }
    }
}