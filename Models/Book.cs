using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books2Gather.Models
{
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

        [ForeignKey("Author")]
        [Column("AuthorId")]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        [ForeignKey("Genre")]
        [Column("GenreId")]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}
