using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books2Gather.Models {
    [Table("Books")] 
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("BookId")] 
        public int BookId { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("Title")]
        public string Title { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("ISBN")]
        public string ISBN { get; set; }

        [Column("PublishingDate")]
        public DateOnly PublishingDate { get; set; }

        [Column("Prize")]
        public decimal Prize { get; set; }

        public Author Author { get; set; }

        public Genre Genre { get; set; }

        [Column("AuthorId")]
        public int AuthorId { get; set; }

        [Column("GenreId")]
        public int GenreId { get; set; }

        //public List<Author> Authors { get; set; } = new List<Author>();

        //public List<Genre> Genres { get; set; } = new List<Genre>();


        //public string AuthorNames => string.Join(", ", Authors.Select(a => $"{a.FirstName} {a.LastName}"));
        //public string GenreNames => string.Join(", ", Genres.Select(g => g.Description));
    }
}
